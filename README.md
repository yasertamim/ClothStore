# ClothStore
# Documentation
## what i am buiding?
The project is an online clothing store. It is a web based aplication which helps people find and buy clothes and accessories with different variety
on internet. It is usful in the way that makes an easier way to buy clothes and accessories online. 
This application has two moudles the customer and the admin.
Costumer have to register an account in order to complete a purchase, the unregistered user can not be able to make a purchase or place an order, however 
unregistered users have ability to see and view the products details. 
The admin module contains the access of admin on the application. The admin have authority to change every thing on the application. The admin have the ability to add,
delete, update any information regarding the products as well as display all orders and delete them.

## who er the target audiance?
The target audiance is every person who wants to shop clothes online.

## Project' components
### Log in
The user will enter his email and password to view and buy products. There are two types of 
users Admin/Costumer.

### How to view a product?
The user can select the between four alternatives(category) menn,women, babies and accessories, then all products related to the alternative that he selected
will be displayed. there he will choose the product that he wants to buy.

### How to place an order?
-	The user logs in
-	The user selects the category.
-	The user selects an item.
-	The user chooses the amount of the product.
-	The user adds the item to Cart.
-	Since the user is finished shopping, he places order.
-	Then the items in the cart will become an order
-	The user press confirm-payment.
-	The user will be navigated to the payment gateway.
-	Since the payment is completed, the user is navigated back to the My Orders page.

### How to pay for the order?
the user can pay throuw credit cards.


## Project features:
### Costumer
- create new account
- log in
- select product
- display the details of a product
- place order
- pay for order
- delete order
### Admin
- log in
- select product
- display the details of a product
- add product
- update product
- delete product
- display all orders
- delete order

## User stories (Costumer):
- As a user i want to be able to register and log in so that so thst i can see all products 
 place order and pay for it
- As a user i want to be able to search for products
- As a user i want to be able to delete my orders

## Pages in the application:
### Views (Customer):
   -	Index (landing page-customer-admin) Home
   -	Index (display products after category) 
   -	Details (display product information)
   -	Cart (list products)
   - ShoppingCart
   - PlaceOrder
   - CompleteOrder
   -	DeleteItem (display item will be deleted)
   -	MyOrder (display the current order after placing order from Cart)
   -	Orders (display all orders)
   -	DeleteOrder (display selected order)
   -	Rating (give rate to a selected product)
   -	Payment (display payment information)
   
### Views (Admin):
   -	Index (landing page-customer-admin) Home
   -	Index (display products after category) 
   -	Details (display product information with options)
   -	EditItem (editing the selected item)
   -	DeleteItem (deleting the selected item)
   -	ManageOrders (display all orders in database with ability to filter after user id)
   -	UserOrders (display the selected order)
   -	DeleteOrder (delete the selected order)



## Project requirments:
- Software:
          - IDE Visual Studio 2022
          - programing language C#.
          (backend)
          - Database Sql Server
          - Front end Html, Css
              - Frameworks:
                  - Frontend: Bootstrap
                  - Backend: asp.net core 6 mvc
                  

## Entity Relationships Diagram
![EDR](https://user-images.githubusercontent.com/60326230/217504026-a52829a9-e3db-40cf-84f9-b6b9c00ce056.png)
