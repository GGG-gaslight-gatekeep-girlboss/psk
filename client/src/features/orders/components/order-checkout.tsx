import { Button, Container, Group, Paper, Stack, Stepper, Table, Text, Title } from "@mantine/core";
import { DatePicker, getTimeRange, TimeGrid } from "@mantine/dates";
import { useState } from "react";
import { Navigate, useNavigate } from "react-router-dom";
import { paths } from "../../../shared/config/paths";
import { formatPrice } from "../../../shared/utils/format";
import { useUserStore } from "../../users/user-store";
import { useCartStore } from "../cart-store";
import { StripeCheckout } from "./stripe-checkout";

export const OrderCheckout = () => {
  const [activeStep, setActiveStep] = useState<number>(0);
  const [date, setDate] = useState<Date>(new Date());
  const [time, setTime] = useState<string | null>(null);
  const navigate = useNavigate();
  const currentUser = useUserStore((state) => state.user!);
  const cartItems = useCartStore((state) => state.items);
  const cartTotal = useCartStore((state) => state.totalPrice);

  const now = new Date();

  const datesAreOnSameDay = (first: Date, second: Date) =>
    first.getFullYear() === second.getFullYear() &&
    first.getMonth() === second.getMonth() &&
    first.getDate() === second.getDate();

  const getMinTime = () => {
    const isTodaySelected = datesAreOnSameDay(date, now);
    if (!isTodaySelected) {
      return "10:00";
    }

    const hours = now.getHours();
    const minutes = now.getMinutes();
    return `${hours}:${minutes}`;
  };

  const getPickupTime = () => {
    if (!time) {
      return null;
    }

    const pickupTime = new Date(date);
    const splitTime = time.split(":").map((x) => parseInt(x));
    pickupTime.setHours(splitTime[0]);
    pickupTime.setMinutes(splitTime[1]);
    pickupTime.setSeconds(splitTime[2]);

    return pickupTime;
  };

  const onNextStepClick = () => {
    setActiveStep((current) => (current < 3 ? current + 1 : current));
  };

  const onPrevStepClick = () => {
    if (activeStep === 0) {
      navigate(paths.cart.getHref());
    } else {
      setActiveStep((current) => (current > 0 ? current - 1 : current));
    }
  };

  const cartItemRows = cartItems.map((item) => (
    <Table.Tr>
      <Table.Td>{item.productName}</Table.Td>
      <Table.Td>{formatPrice(item.productPrice)}</Table.Td>
      <Table.Td>{item.quantity}</Table.Td>
      <Table.Td>{formatPrice(item.totalPrice)}</Table.Td>
    </Table.Tr>
  ));

  if (cartItems.length === 0) {
    return <Navigate to={paths.home.getHref()} />;
  }

  return (
    <Container>
      <Title order={3} my="md">
        Place order
      </Title>
      <Paper withBorder p="xl">
        <Stepper active={activeStep} onStepClick={setActiveStep}>
          <Stepper.Step label="Pickup day & time" mt="sm">
            <Group justify="space-around" align="start">
              <Paper withBorder>
                <DatePicker value={date} onChange={(x) => setDate(new Date(x))} minDate={now} />
              </Paper>
              <Paper mah={320} style={{ overflowY: "auto", overflowX: "hidden" }}>
                <TimeGrid
                  data={getTimeRange({
                    startTime: "10:00",
                    endTime: "20:00",
                    interval: "00:10",
                  })}
                  simpleGridProps={{
                    cols: 1,
                    spacing: "xs",
                    w: 360,
                  }}
                  minTime={getMinTime()}
                  withSeconds={false}
                  value={time}
                  onChange={setTime}
                  allowDeselect
                />
              </Paper>
            </Group>
          </Stepper.Step>

          <Stepper.Step label="Order summary">
            <Stack gap={0}>
              <Text>First name: {currentUser.firstName}</Text>
              <Text>Last name: {currentUser.lastName}</Text>
              <Text>Phone number: {currentUser.phoneNumber}</Text>
              <Text>
                Pickup time: {date.toLocaleDateString()}, {time?.slice(0, 5)}
              </Text>
            </Stack>

            <Table withTableBorder withRowBorders striped my="md">
              <Table.Thead>
                <Table.Tr>
                  <Table.Th>Product</Table.Th>
                  <Table.Th>Price</Table.Th>
                  <Table.Th>Quantity</Table.Th>
                  <Table.Th>Total</Table.Th>
                </Table.Tr>
              </Table.Thead>
              <Table.Tbody>{cartItemRows}</Table.Tbody>
            </Table>

            <Text>Total price: {formatPrice(cartTotal)}</Text>
          </Stepper.Step>

          <Stepper.Step label="Payment">
            <StripeCheckout getPickupTime={getPickupTime} onBackClick={onPrevStepClick} />
          </Stepper.Step>
        </Stepper>

        {activeStep !== 2 && (
          <Group justify="space-between" mt="lg">
            <Button variant="default" onClick={onPrevStepClick}>
              {activeStep === 0 ? "Cancel" : "Back"}
            </Button>
            <Button onClick={onNextStepClick} disabled={!time}>
              Next
            </Button>
          </Group>
        )}
      </Paper>
    </Container>
  );
};
