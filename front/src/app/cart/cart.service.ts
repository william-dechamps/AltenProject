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

        // Find the product in the cart
        const existingItem = this.cart.cartItems.find(
            (cart) => cart.product.id === product.id
        );

        // If the product is already in the cart
        if (existingItem) {
            existingItem.quantity += 1;
        } else {
            // If the product is not in the cart, add it with a quantity of 1
            this.cart.cartItems.push({ product, quantity: 1 });
        }

        // Emit the updated cart items
        this._cart.next(this.cart.cartItems);

        // Emit the new length of the cart
        this.cartSubject.next(this.cart.cartItems.length);
    }

    updateCart(product: Product, newQuantity: number) {
        // Find the product in the cart
        const existingItem = this.cart.cartItems.find(
            (cart) => cart.product.id === product.id
        );

        // If the product is already in the cart
        if (existingItem) {
            const difference = newQuantity - existingItem.quantity;
            let result = existingItem.product.quantity;

            if (difference > 0) {
                result -= difference;
            } else if (difference < 0) {
                result += -difference;
            }

            if (result < 0) {
                result = 0;
            }

            this.cart.cartItems = this.cart.cartItems.map((cart) => {
                if (cart.product.id === product.id) {
                    return {
                        ...cart,
                        product: {
                            ...cart.product,
                            quantity: result,
                        },
                        quantity: newQuantity,
                    };
                }
                return cart;
            });

            // Update the quantity of the product in the cart
            existingItem.quantity = newQuantity;

            // If the quantity is 0, remove the product from the cart
            if (existingItem.quantity <= 0) {
                this.cart.cartItems = this.cart.cartItems.filter(
                    (cart) => cart.product.id !== product.id
                );
            }
        }

        // Emit the updated cart items
        this._cart.next(this.cart.cartItems);

        // Emit the new length of the cart
        this.cartSubject.next(this.cart.cartItems.length);
        console.log(this.cart.cartItems);
    }

    getCartItems(): Observable<CartItem[]> {
        return this.cart$;
    }
}