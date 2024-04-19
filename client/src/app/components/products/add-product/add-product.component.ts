import { Component, NgModule } from '@angular/core';
import { Product } from '../../../models/product.model';
import { FormBuilder, FormsModule, NgModel, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule, NgIf } from '@angular/common';
import { ProductDto } from '../../../models/product.dto';
import { ProductService } from '../../../services/product.service';
import { Toast, ToastrService } from 'ngx-toastr';
import { BrowserModule } from '@angular/platform-browser';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-product',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf, CommonModule],
  templateUrl: './add-product.component.html',
  styleUrl: './add-product.component.scss'
})
export class AddProductComponent {
  productForm = this.fb.group({
    name: ['', Validators.required],
    price: [0, Validators.required],
    code: ['', Validators.required],
    image: [null]
  });
  formData = new FormData;

  constructor(private fb: FormBuilder, private productService: ProductService, private toastr: ToastrService, private router: Router) { }

  onFileChange(event: any) {
    this.formData = new FormData();
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.formData.append('image', file);
    }
  }

  onSubmit() {
    this.formData.append('name', this.productForm.get('name')!.value!);
    this.formData.append('price', this.productForm.get('price')!.value!.toString());
    this.formData.append('code', this.productForm.get('code')!.value!);

    this.productService.addProduct(this.formData).subscribe({
      next: response => {
        this.toastr.success('Product added successfully');
        this.productForm.reset();
        this.router.navigate(['/products']);
      },
      error: err => {
        this.toastr.error(err.error, 'Failed to add product');
      }
    });
  }
}
