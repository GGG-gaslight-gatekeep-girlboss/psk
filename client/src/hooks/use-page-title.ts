import { useEffect } from "react";

export const usePageTitle = ({ title }: { title: string | null }) => {
  const suffix = "Coffee Shop";

  useEffect(() => {
    document.title = title ? `${title} | ${suffix}` : suffix;
  }, []);
};
