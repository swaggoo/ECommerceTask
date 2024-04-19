import { OrderStatus } from "../enums/orderStatus.enum";

export interface OrderTableModel {
    orderId: string;
    customerFullname: string;
    customerPhone: string;
    date: string;
    totalPrice: string;
    status: OrderStatus;
}