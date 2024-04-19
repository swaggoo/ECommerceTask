export class OrderDto {
    customerFullName?: string;
    customerPhone?: string;
    productQuantities: { [productId: number]: number } = {};
}