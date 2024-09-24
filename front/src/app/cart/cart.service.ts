import { Injectable } from "@angular/core";
import { Product } from "app/products/data-access/product.model";
import { BehaviorSubject, Observable, } from "rxjs";
import { Cart, CartItem } from "./data-access/cart.model";

@Injectable({
    providedIn: "root"
})
export class CartService {
    private cart: Cart = { cartItems: [] };
    private cartSubject = new BehaviorSubject<number>(0);
    private _cart = new BehaviorSubject<CartItem[]>(this.cart.cartItems);
    cart$ = this._cart.asObservable();
    cartSubject$: Observable<number> = this.cartSubject.asObservable();

    addToCart(product: Product) {
        // Decrease the product quantity
        product.quantity -= 1;

        // Add the product to the cart
        this.updateCart(product, 1);
    }

    updateCart(product: Product, change: number) {
        // Find the product in the cart
        const existingItem = this.cart.cartItems.find(
            (cart) => cart.product.id === product.id
        );

        // If the product is already in the cart
        if (existingItem) {
            // Update the quantity of the product in the cart
            existingItem.quantity += change;

            // If the quantity is 0, remove the product from the cart
            if (existingItem.quantity === 0) {
                this.cart.cartItems = this.cart.cartItems.filter(
                    (cart) => cart.product.id !== product.id
                );
            }

            // Emit the new length of the cart
            this.cartSubject.next(this.cart.cartItems.length);
        } else {
            // If the product is not in the cart, add it
            this.cart.cartItems.push({ product, quantity: change });

            // Emit the new length of the cart
            this.cartSubject.next(this.cart.cartItems.length);
        }

        this._cart.next(this.cart.cartItems);
    }

    getCartItems(): Observable<CartItem[]> {
        return this.cart$;
    }
}