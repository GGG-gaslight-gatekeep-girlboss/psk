import { useEffect } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { showInfoNotification } from '../utils/notifications';
import { formatDate} from "../utils/format";

function getCookie(name: string): string | null {
  const match = document.cookie.match(new RegExp('(^| )' + name + '=([^;]+)'));
  return match ? decodeURIComponent(match[2]) : null;
}

type OrderReadyPayload = {
  orderId: string;
  pickupTime: string;
};

export const useOrderNotifications = () => {
  useEffect(() => {
    const token = getCookie("CoffeeShopAccessToken");

    const connection = new HubConnectionBuilder()
    .withUrl(`http://localhost:5000/hub/notifications?access_token=${token ?? ''}`)
    .build();

    connection
      .start()
      .then(() => {
        connection.on('orderReady', (payload: OrderReadyPayload) => {
          showInfoNotification({
            message: `Order #${payload.orderId} is ready for pickup at ${formatDate(payload.pickupTime)}!`,
          });
        });
      })
      .catch((err) => {
        console.error('SignalR connection failed', err);
      });

    return () => {
      connection?.stop();
    };
  }, []);
};