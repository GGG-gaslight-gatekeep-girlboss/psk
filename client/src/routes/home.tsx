import { usePageTitle } from "../shared/hooks/use-page-title";

export const HomeRoute = () => {
  usePageTitle({ title: null });

  return <div>Hello home</div>;
};
