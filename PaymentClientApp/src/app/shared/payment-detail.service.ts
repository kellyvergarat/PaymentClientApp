import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { PaymentDetail } from './payment-detail.model';
import { NgForm } from '@angular/forms';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PaymentDetailService {

  url:string = environment.apiBaseUrl+'/PaymentDetail'
  list:PaymentDetail[] = []
  formData : PaymentDetail = new PaymentDetail()
  formSubmitted:boolean = false;

  constructor(private http: HttpClient ) {}

  refreshList(){
    this.http.get(this.url).subscribe({
      next: res=>{
        this.list = res as PaymentDetail[]
      },
      error: err => {console.log(err)}
    })
  }

  postPaymentDetail(){
    return this.http.post(this.url, this.formData)
  }

  deletePaymentDetail(id:string){
    return this.http.delete(this.url + '/' +id)
  }

  putPaymentDetail(paymentDetail: PaymentDetail): Observable<PaymentDetail> {
    return this.http.put<PaymentDetail>(this.url, paymentDetail);
  }

   resetForm(form: NgForm){
    form.form.reset()
    this.formData = new PaymentDetail()
    this.formSubmitted = false
   }
}
