import { create } from "zustand";
import { Product } from "../products/types";
import { OrderItem } from "./types";

type CartState = {
  items: OrderItem[];
  totalPrice: number;
  addToCart: (product: Product, quantity: number) => void;
  removeFromCart: (productId: string) => void;
};

export const useCartStore = create<CartState>((set) => ({
  items: [],
  totalPrice: 0,
  addToCart: (product, quantity) =>
    set((state) => {
      const prevItems = state.items.filter((x) => x.productId !== product.id);
      const newItems: OrderItem[] = [
        ...prevItems,
        {
          productId: product.id,
          productName: product.name,
          productPrice: product.price,
          quantity: quantity,
          totalPrice: product.price * quantity,
        },
      ];
      const totalPrice = newItems.reduce((acc, item) => acc + item.totalPrice, 0);

      return { items: newItems, totalPrice: totalPrice };
    }),
  removeFromCart: (productId) =>
    set((state) => {
      const items = state.items.filter((x) => x.productId !== productId);
      const totalPrice = items.reduce((acc, item) => acc + item.totalPrice, 0);
      return { items, totalPrice };
    }),
}));
