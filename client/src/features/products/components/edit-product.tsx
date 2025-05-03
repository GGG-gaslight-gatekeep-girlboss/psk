import {
  Button,
  FileInput,
  Group,
  Modal,
  NumberInput,
  Paper,
  Stack,
  Text,
  Textarea,
  TextInput,
  Title,
} from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";
import { useDisclosure } from "@mantine/hooks";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { paths } from "../../../shared/config/paths";
import { showSuccessNotification } from "../../../shared/utils/notifications";
import { useDeleteProduct } from "../api/delete-product";
import { EditProductInput, editProductInputSchema, useEditProduct } from "../api/edit-product";
import { useProduct } from "../api/get-product";
import { useSetProductImage } from "../api/set-product-image";

export const EditProduct = (props: { productId: string }) => {
  const [image, setImage] = useState<File | null>(null);
  const [isDeleteModalOpen, { open: openDeleteModal, close: closeDeleteModal }] = useDisclosure(false);
  const productQuery = useProduct({ params: { productId: props.productId } });
  const navigate = useNavigate();

  const editProductForm = useForm<EditProductInput>({
    mode: "uncontrolled",
    initialValues: {
      name: "",
      description: "",
      price: 0,
      stock: 0,
    },
    validate: zodResolver(editProductInputSchema),
  });

  const setProductImageMutation = useSetProductImage({
    mutationConfig: {
      onSuccess: () => {
        showSuccessNotification({ message: "Image updated successfully" });
        setImage(null);
      },
    },
  });

  const editProductMutation = useEditProduct({
    mutationConfig: {
      onSuccess: () => {
        showSuccessNotification({ message: "Product updated successfully" });

        if (image) {
          setProductImageMutation.mutate({ product: productQuery.data!, image });
        }
      },
    },
  });

  const deleteProductMutation = useDeleteProduct({
    mutationConfig: {
      onSuccess: () => {
        showSuccessNotification({ message: "Product deleted successfully" });
        navigate(paths.products.getHref());
      },
    },
  });

  useEffect(() => {
    if (productQuery.data) {
      editProductForm.setValues(productQuery.data);
    }
  }, [productQuery.data]);

  const handleEditProduct = (data: EditProductInput) => {
    editProductMutation.mutate({ productId: props.productId, data });
  };

  const handleDeleteProduct = () => {
    deleteProductMutation.mutate({ productId: props.productId });
  };

  if (productQuery.isLoading) {
    return <Text>Loading...</Text>;
  }

  if (!productQuery.data) {
    return <Text>Something went wrong...</Text>;
  }

  return (
    <>
      <Modal opened={isDeleteModalOpen} onClose={closeDeleteModal} centered title="Delete product">
        <Text>Are you sure you want to delete the product?</Text>

        <Group mt="lg" justify="end">
          <Button variant="default" onClick={closeDeleteModal}>
            Cancel
          </Button>
          <Button color="red" onClick={handleDeleteProduct}>
            Delete
          </Button>
        </Group>
      </Modal>

      <Paper withBorder p="xl" radius="md">
        <Title order={4} mb="md">
          Edit product
        </Title>

        <form onSubmit={editProductForm.onSubmit(handleEditProduct)}>
          <Stack gap="xs">
            <TextInput
              label="Name"
              placeholder="Enter product name"
              required
              key={editProductForm.key("name")}
              {...editProductForm.getInputProps("name")}
            />
            <Textarea
              label="Description"
              placeholder="Enter product description"
              required
              autosize
              minRows={3}
              maxRows={5}
              key={editProductForm.key("description")}
              {...editProductForm.getInputProps("description")}
            />
            <NumberInput
              label="Stock"
              placeholder="Enter product stock"
              required
              key={editProductForm.key("stock")}
              {...editProductForm.getInputProps("stock")}
            />
            <NumberInput
              label="Price"
              placeholder="Enter product price"
              suffix="€"
              required
              key={editProductForm.key("price")}
              {...editProductForm.getInputProps("price")}
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

          <Group mt="xl" justify="space-between">
            <Button color="red" onClick={openDeleteModal}>
              Delete
            </Button>

            <Group gap="xs">
              <Button variant="default" onClick={() => navigate(paths.products.getHref())}>
                Cancel
              </Button>
              <Button color="teal" type="submit">
                Save
              </Button>
            </Group>
          </Group>
        </form>
      </Paper>
    </>
  );
};
