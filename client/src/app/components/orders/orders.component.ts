import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { OrderTableModel } from '../../models/order-table.model';
import { Subscription } from 'rxjs';
import { OrderService } from '../../services/order.service';
import { Pagination } from '../../models/pagination';
import { NgbPagination } from '@ng-bootstrap/ng-bootstrap';
import { NgxPaginationModule } from 'ngx-pagination';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule, NgbPagination, NgxPaginationModule],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.scss'
})
export class OrdersComponent implements OnInit, OnDestroy {
  subsription = new Subscription();
  orders: OrderTableModel[] = [];
  pageNumber = 1;
  pageSize = 8;
  pagination?: Pagination;

  constructor(private readonly orderService: OrderService) { }

  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders() {
    this.subsription.add(this.orderService.getOrdersPerPage(this.pageNumber, this.pageSize).subscribe({
      next: response => {
        if (response.result && response.pagination) {
          this.orders = response.result;
          this.pagination = response.pagination;
        }
      }
    }));
  }

  pageChanged(event: any) {
    if (this.pageNumber !== event) {
      this.pageNumber = event;
      this.loadOrders();
    }
  }

  ngOnDestroy(): void {
    this.subsription.unsubscribe();
  }
}
