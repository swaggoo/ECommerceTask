import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { OrderService } from '../../../services/order.service';
import { OrderDto } from '../../../models/order.model';

@Component({
  selector: 'app-add-order',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './add-order.component.html',
  styleUrl: './add-order.component.scss'
})
export class AddOrderComponent {
  orderDto = new OrderDto();
  orderForm = this.fb.group({
    customerFullName: ['', Validators.required],
    customerPhone: ['', Validators.required]
  });

  constructor(private fb: FormBuilder,
    private toastr: ToastrService,
    private orderService: OrderService,
    private router: Router) { }

  onSubmit() {
    if (this.orderForm.valid) {
      const formData = this.orderForm.value;

      const productQuantities = this.orderService.getOrder();
      this.orderDto.productQuantities = { ...productQuantities };
      this.orderDto.customerFullName = formData.customerFullName as string;
      this.orderDto.customerPhone = formData.customerPhone as string;

      this.orderService.createOrder(this.orderDto).subscribe({
        next: response => {
          this.orderForm.reset();
          this.orderService.clearOrder();
          this.toastr.success('Order created successfully');
          this.router.navigate(['/orders']);
        },
        error: err => {
          this.toastr.error(err.error, 'Failed to create order');
        }
      })
    } else {
      this.toastr.error('Please fill in all required fields.');
    }
  }
}
