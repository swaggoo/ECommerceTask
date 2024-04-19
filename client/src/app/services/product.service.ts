import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { Product } from '../models/product.model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PaginatedResult } from '../models/pagination';
import { environment } from '../../environments/environment.development';
import { ProductDto } from '../models/product.dto';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl = '/api/products/';
  private apiUrl = environment.apiUrl + this.baseUrl;
  paginatedResult: PaginatedResult<Product[]> = new PaginatedResult<Product[]>();

  constructor(private readonly http: HttpClient) { }

  getProductsPerPage(page?: number, itemsPerPage?: number) {
    console.log(this.apiUrl);
    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    return this.http.get<Product[]>(this.apiUrl, { observe: 'response', params }).pipe(
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

  addProduct(productDto: FormData) {
    return this.http.post(this.apiUrl, productDto );
  }
}
