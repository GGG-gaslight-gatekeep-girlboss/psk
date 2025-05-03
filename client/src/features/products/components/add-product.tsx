import { Button, FileInput, NumberInput, Paper, Stack, Textarea, TextInput, Title } from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";
import { useState } from "react";
import { showInfoNotification, showSuccessNotification } from "../../../shared/utils/notifications";
import { AddProductInput, addProductInputSchema, useAddProduct } from "../api/add-product";
import { useSetProductImage } from "../api/set-product-image";
import { Product } from "../types";

export const AddProduct = (props: { onProductAdded: (product: Product) => void }) => {
  const [image, setImage] = useState<File | null>(null);

  const addProductForm = useForm<AddProductInput>({
    mode: "uncontrolled",
    initialValues: {
      name: "",
      description: "",
      price: 0,
      stock: 0,
    },
    validate: zodResolver(addProductInputSchema),
  });

  const setProductImageMutation = useSetProductImage({
    mutationConfig: {
      onSuccess: () => {
        showSuccessNotification({ message: "Product image uploaded successfully" });
      },
      onSettled: (_, _1, { product }) => {
        props.onProductAdded(product);
      },
    },
  });

  const addProductMutation = useAddProduct({
    mutationConfig: {
      onSuccess: (product) => {
        showSuccessNotification({ message: "Product added successfully" });

        if (image) {
          showInfoNotification({ message: "Uploading product image..." });
          setProductImageMutation.mutate({ product, image });
        } else {
          props.onProductAdded(product);
        }
      },
    },
  });

  const handleAddProduct = (data: AddProductInput) => {
    addProductMutation.mutate({ data });
  };

  return (
    <Paper withBorder p="xl" radius="md">
      <Title order={4} mb="md">
        Add product
      </Title>

      <form onSubmit={addProductForm.onSubmit(handleAddProduct)}>
        <Stack gap="xs">
          <TextInput
            label="Name"
            placeholder="Enter product name"
            required
            key={addProductForm.key("name")}
            {...addProductForm.getInputProps("name")}
          />
          <Textarea
            label="Description"
            placeholder="Enter product description"
            required
            autosize
            minRows={3}
            maxRows={5}
            key={addProductForm.key("description")}
            {...addProductForm.getInputProps("description")}
          />
          <NumberInput
            label="Stock"
            placeholder="Enter product stock"
            required
            key={addProductForm.key("stock")}
            {...addProductForm.getInputProps("stock")}
          />
          <NumberInput
            label="Price"
            placeholder="Enter product price"
            suffix="€"
            required
            key={addProductForm.key("price")}
            {...addProductForm.getInputProps("price")}
          />
          <FileInput
            label="Image"
            placeholder="Click to upload image"
            accept="image/png,image/jpeg,image/jpg"
            clearable
            value={image}
            onChange={setImage}
          />
        </Stack>

        <Button
          fullWidth
          mt="xl"
          type="submit"
          loading={addProductMutation.isPending || setProductImageMutation.isPending}
        >
          Add product
        </Button>
      </form>
    </Paper>
  );
};
