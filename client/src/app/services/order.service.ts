import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { OrderDto } from '../models/order.model';
import { OrderTableModel } from '../models/order-table.model';
import { PaginatedResult } from '../models/pagination';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private baseUrl = '/api/orders/';
  private apiUrl = environment.apiUrl + this.baseUrl;

  private orderSubject = new BehaviorSubject<{ [productId: number]: number }>({});
  order$ = this.orderSubject.asObservable();

  private totalQuantitySubject = new BehaviorSubject<number>(0);
  totalQuantity$ = this.totalQuantitySubject.asObservable();
  paginatedResult: PaginatedResult<OrderTableModel[]> = new PaginatedResult<OrderTableModel[]>();

  constructor(private http: HttpClient) { }

  addToOrder(productId: number): void {
    const currentOrder = this.orderSubject.getValue();
    if (currentOrder[productId]) {
      currentOrder[productId]++;
    } else {
      currentOrder[productId] = 1;
    }
    this.orderSubject.next(currentOrder);
    this.updateTotalQuantity();
  }

  clearOrder(): void {
    this.orderSubject.next({});
  }

  removeFromOrder(productId: number): void {
    const currentOrder = this.orderSubject.getValue();
    if (currentOrder[productId]) {
      currentOrder[productId]--;
      if (currentOrder[productId] === 0) {
        delete currentOrder[productId];
      }
      this.orderSubject.next(currentOrder);
      this.updateTotalQuantity();
    }
  }

  updateTotalQuantity(): void {
    const currentOrder = this.orderSubject.getValue();
    const totalQuantity = Object.values(currentOrder).reduce((a, b) => a + b, 0);
    this.totalQuantitySubject.next(totalQuantity);
  }

  getOrder(): { [productId: number]: number } {
    return this.orderSubject.getValue();
  }

  createOrder(orderDto: OrderDto): Observable<any> {
    return this.http.post(this.apiUrl, orderDto);
  }

  getOrdersPerPage(page?: number, itemsPerPage?: number) {
    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    return this.http.get<OrderTableModel[]>(this.apiUrl + 'perPage', { observe: 'response', params }).pipe(
      map(response => {
        if (response.body) {
          this.paginatedResult.result = response.body;
        }
        const pagination = response.headers.get('Pagination');
        if (pagination) {
          this.paginatedResult.pagination = JSON.parse(pagination);
        }
        return this.paginatedResult;
      })
    );
  }
}
