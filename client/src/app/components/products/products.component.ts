import { CommonModule, NgFor } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ProductItemComponent } from './product-item/product-item.component';
import { Product } from '../../models/product.model';
import { Pagination } from '../../models/pagination';
import { ProductService } from '../../services/product.service';
import { NgxPaginationModule } from 'ngx-pagination';
import { AddProductComponent } from './add-product/add-product.component';
import { Subscription, partition } from 'rxjs';
import { OrderService } from '../../services/order.service';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [RouterModule, CommonModule, ProductItemComponent, NgxPaginationModule, AddProductComponent],
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss'
})
export class ProductsComponent implements OnInit, OnDestroy {
  private subsription = new Subscription();

  products: Product[] = [];
  pagination?: Pagination;
  pageNumber = 1;
  pageSize = 4;
  totalQuantity: number = 0;

  constructor(private readonly productService: ProductService, private readonly orderService: OrderService) { }


  ngOnInit(): void {
    this.loadProducts();

    this.subsription.add(this.orderService.totalQuantity$.subscribe(quantity => {
      this.totalQuantity = quantity;
    }));

    this.orderService.totalQuantity$.subscribe(quantity => {
      this.totalQuantity = quantity;
    });

  }

  loadProducts() {
    this.subsription.add(this.productService.getProductsPerPage(this.pageNumber, this.pageSize).subscribe({
      next: response => {
        if (response.result && response.pagination) {
          this.products = response.result;
          this.pagination = response.pagination;
        }
      }
    }));
  }

  pageChanged(event: any) {
    if (this.pageNumber !== event) {
      this.pageNumber = event;
      this.loadProducts();
    }
  }

  ngOnDestroy(): void {
    this.subsription.unsubscribe();
  }
}
