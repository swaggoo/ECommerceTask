import { Routes } from '@angular/router';
import { AddProductComponent } from './components/products/add-product/add-product.component';
import { ProductsComponent } from './components/products/products.component';
import { AddOrderComponent } from './components/add-order/add-order.component';
import { OrdersComponent } from './components/orders/orders.component';

export const routes: Routes = [
    { path: '', redirectTo: 'products', pathMatch: 'full' },
    { path: 'products', component: ProductsComponent },
    { path: 'add-product', component: AddProductComponent },
    { path: 'order', component: AddOrderComponent},
    { path: 'orders', component: OrdersComponent},
];
