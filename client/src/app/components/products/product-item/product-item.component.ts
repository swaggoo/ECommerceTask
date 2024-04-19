import { Component, Input } from '@angular/core';
import { NgIf } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Product } from '../../../models/product.model';
import { OrderService } from '../../../services/order.service';

@Component({
  selector: 'app-product-item',
  standalone: true,
  imports: [NgIf, RouterModule],
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.scss'
})
export class ProductItemComponent {
  @Input() product?: Product;
  quantity: number = 0;

  constructor(private orderService: OrderService) { }

  decreaseQuantity() {
    if (this.quantity > 0) {
      this.quantity--;
      this.orderService.removeFromOrder(this.product!.id);
    }
  }

  increaseQuantity() {
    this.quantity++;
    this.orderService.addToOrder(this.product!.id);
  }
}
