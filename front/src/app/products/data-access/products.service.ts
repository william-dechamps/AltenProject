import { Injectable, inject, signal } from "@angular/core";
import { Product } from "./product.model";
import { HttpClient } from "@angular/common/http";
import { catchError, Observable, tap } from "rxjs";
import { environment } from '../../../environments/environment';

@Injectable({
    providedIn: "root"
})

export class ProductsService {
    private readonly http = inject(HttpClient);
    private readonly API_PATH = environment.API_PATH;
    private readonly _products = signal<Product[]>([]);
    public readonly products = this._products.asReadonly();

    public get(): Observable<Product[]> {
        return this.http.get<Product[]>(this.API_PATH + "/products/getproducts").pipe(
            catchError((error) => {
                return [];
            }),
            tap((products) => this._products.set(products)),
        );
    }
}