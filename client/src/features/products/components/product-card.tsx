import { Group, Image, Paper, Stack, Text } from "@mantine/core";
import { formatPrice } from "../../../shared/utils/format";
import { Product } from "../types";

export const ProductCard = (props: { product: Product; onClick: (p: Product) => void }) => {
  const handleClick = () => {
    props.onClick(props.product);
  };

  return (
    <Paper withBorder style={{ overflow: "hidden", cursor: "pointer" }} onClick={handleClick}>
      <Group justify="space-between" gap={0}>
        <Stack p="sm" style={{ width: "calc(100% - 144px)" }}>
          <Text mah={144} lineClamp={3}>
            {props.product.name}
          </Text>
          <Text c="dimmed">{formatPrice(props.product.price)}</Text>
        </Stack>

        <Paper h={144} w={144}>
          <Image
            src={props.product.imageUrl}
            h="100%"
            w="100%"
            fallbackSrc="https://placehold.co/400x400?text=CoffeeShop"
          />
        </Paper>
      </Group>
    </Paper>
  );
};
