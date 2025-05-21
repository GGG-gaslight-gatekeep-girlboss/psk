import { createTheme, MantineProvider } from "@mantine/core";
import "@mantine/core/styles.css";
import { DatesProvider } from "@mantine/dates";
import "@mantine/dates/styles.css";
import { Notifications } from "@mantine/notifications";
import "@mantine/notifications/styles.css";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import App from "./App.tsx";
import { queryConfig } from "./shared/config/react-query.ts";

const queryClient = new QueryClient({ defaultOptions: queryConfig });

const theme = createTheme({
  primaryShade: 7,
});

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <QueryClientProvider client={queryClient}>
      <MantineProvider theme={theme}>
        <DatesProvider settings={{}}>
          <Notifications />
          <App />
        </DatesProvider>
      </MantineProvider>
    </QueryClientProvider>
  </StrictMode>
);
