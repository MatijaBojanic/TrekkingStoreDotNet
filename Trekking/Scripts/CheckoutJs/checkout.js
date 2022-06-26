

var order = null
var orders = []
var products = []

let trekkinguser = JSON.parse(localStorage.getItem('trekkinguser'));
if(trekkinguser) {
    userId = trekkinguser.UserId
    fetchOrderJSON(userId).then(result=>{
        console.log(result)
        order = result;
        fetchOrderItemsJSON(order).then(orderItems=>{
            fetchProductsJSON().then(result=>{
                products = result
                console.log(orderItems)
                createRowsForOrderItems(orderItems, products)
                getTotalPriceAndNumOfItems(orderItems, products)
            })
        })
    })
}

async function fetchProductsJSON() {
    const response = await fetch('http://localhost:5000/api/products');
    return await response.json();
}

async function fetchOrderJSON(userId) {
    const response = await fetch('http://localhost:5000/api/user/'+userId+'/order');
    return await response.json();
}

async function fetchOrderItemsJSON(order) {
    //api/order/{orderId}/order-items
    const response = await fetch('/api/order/'+order.OrderId+'/order-items');
    return await response.json();
}

function createRowsForOrderItems(orderItems, products){
    for(let i = 1; i <= orderItems.length; i++ ){
        let orderItem = orderItems[i-1]
        let product = products.find(product => product.ProductId === orderItem.ProductId)
        console.log(products)
        let rowContainer = document.createElement('div');
        rowContainer.classList.add('row')
        rowContainer.classList.add('mb-4')
        rowContainer.classList.add('d-flex')
        rowContainer.classList.add('justify-content-center')
        rowContainer.classList.add('align-items-center')
        
        let imgContainer = document.createElement('div')
        imgContainer.classList.add('col-md-2')
        imgContainer.classList.add('col-lg-2')
        imgContainer.classList.add('col-xl-2"')
        let img = document.createElement('img')
        img.src = "/Content/Resources/product-1.png"
        img.classList.add("img-fluid")
        img.classList.add("rounded-3")
        img.alt="Cotton T-shirt"
        
        let nameContainer = document.createElement('div')
        nameContainer.classList.add("col-md-3")
        nameContainer.classList.add("col-lg-3")
        nameContainer.classList.add("col-xl-3")
        let name = document.createElement("h6")
        name.classList.add("text-muted")
        name.innerText = product.Name//"Shirt"
        let description = document.createElement("h6")
        description.classList.add("text-black")
        description.classList.add("mb-0")
        description.innerText = product.Description?.substring(30) +"..."
        
        let buttonContainer = document.createElement('div')
        buttonContainer.classList.add("col-md-3")
        buttonContainer.classList.add("col-lg-3")
        buttonContainer.classList.add("col-xl-3")
        buttonContainer.classList.add("d-flex")
        let minusButton = document.createElement('button')
        minusButton.classList.add("btn")
        minusButton.classList.add("btn-link")
        minusButton.classList.add("px-2")
        minusButton.onclick = function(){
            this.parentNode.querySelector('input[type=number]').stepDown()
            $.ajax({
                type:"PATCH",
                url:"http://localhost:5000/api/order-items",
                data:{ OrderItemId:orderItem.OrderItemId, Quantity: this.parentNode.querySelector('input[type=number]').value},
                success: function(data){
                }
            })
            orderItem.Quantity = this.parentNode.querySelector('input[type=number]').value
            getTotalPriceAndNumOfItems(orderItems, products)
        }
        minusButton.innerText = "-"
        
        
        let quantityInput = document.createElement("input")
        quantityInput.id = "form" + i
        quantityInput.setAttribute("min", "0")
        quantityInput.setAttribute("name", "quantity")
        quantityInput.setAttribute("value", orderItem.Quantity)
        quantityInput.setAttribute("type", "number")
        quantityInput.classList.add("form-control")
        quantityInput.classList.add("form-control-sm")
        let plusButton = document.createElement('button')
        plusButton.classList.add("btn")
        plusButton.classList.add("btn-link")
        plusButton.classList.add("px-2")
        plusButton.onclick = function(){
            this.parentNode.querySelector('input[type=number]').stepUp()
            $.ajax({
                type:"PATCH",
                url:"http://localhost:5000/api/order-items",
                data:{ OrderItemId:orderItem.OrderItemId, Quantity: this.parentNode.querySelector('input[type=number]').value},
                success: function(data){
                }
            })
            orderItem.Quantity = this.parentNode.querySelector('input[type=number]').value
            getTotalPriceAndNumOfItems(orderItems, products)
        }
        
        
        plusButton.innerText = "+"
        
        let priceContainer = document.createElement('div')
        priceContainer.classList.add("col-md-3")
        priceContainer.classList.add("col-lg-2")
        priceContainer.classList.add("col-xl-2")
        priceContainer.classList.add("offset-lg-1")
        let price = document.createElement("h6")
        price.classList.add("mb-0")
        price.innerText = "€ " + product.Price
        
        let lineBreak = document.createElement('hr')
        lineBreak.classList.add("my-4")

        
        imgContainer.appendChild(img)
        nameContainer.appendChild(name)
        nameContainer.appendChild(description)
        buttonContainer.appendChild(minusButton)
        buttonContainer.appendChild(quantityInput)
        buttonContainer.appendChild(plusButton)
        priceContainer.appendChild(price)
        rowContainer.appendChild(imgContainer)
        rowContainer.appendChild(nameContainer)
        rowContainer.appendChild(buttonContainer)
        rowContainer.appendChild(priceContainer)
        rowContainer.appendChild(lineBreak)
        $("#itemrows").append(rowContainer)
    }
}

function getTotalPriceAndNumOfItems(orderItems, products){
    let numOfItems = document.getElementById("numberofitems")
    let totalPriceResult = document.getElementById("totalprice")
    numOfItems.innerText = orderItems.length + " items"
    let totalPrice = 0
    for(let i = 0; i < orderItems.length; i++){
        let orderItem = orderItems[i]
        let product = products.find(product => product.ProductId === orderItem.ProductId)
        totalPrice += orderItem.Quantity * product.Price
    }
    totalPriceResult.innerText = "€ " +totalPrice
}

function checkout(){
    
}