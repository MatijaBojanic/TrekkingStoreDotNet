var currentPage = 1
var foundProducts = []
var userId = null
var order = null
let trekkinguser = JSON.parse(localStorage.getItem('trekkinguser'));
if(trekkinguser) {
    userId = trekkinguser.UserId
    fetchOrderJSON(userId).then(result=>{
        console.log(result)
        order = result;
    })
}

fetchProductsJSON().then(products=>{
    currentPage = 1
    foundProducts = products;
    createProductCards(foundProducts)
    createPagers(foundProducts)  
    setPagerActive(1)
    let searchButton = document.getElementById("main-search-button")
    searchButton.onclick = function(){
        currentPage = 1;
        deleteProductCards();
        deletePagers()
        let searchWord = getSearchWord()
        foundProducts = searchProducts(searchWord, products)
        createProductCards(foundProducts)
        createPagers(foundProducts)
        setPagerActive(1)
    }
    
})


async function fetchOrderJSON(userId) {
    console.log('http://localhost:5000/api/user/'+userId+'/order')
    const response = await fetch('http://localhost:5000/api/user/'+userId+'/order');
    return await response.json();
}


async function fetchProductsJSON() {
    const response = await fetch('http://localhost:5000/api/products');
    return await response.json();
}

function createProductCards(products){
    var productTable = document.getElementById('products-table');
    for(let i = 0; i < products.length; i++){
        if(i === 6)
            break
        product = products[i];
        createProductCard(product, productTable)
    }
}

function deleteProductCards(){
    let productCards = document.getElementsByClassName('product-card');
    for(let i = productCards.length - 1; i >= 0; i--) {
        productCards[i].remove();
    }
}

function createProductCard(product, productTable){
    let element = document.createElement('div');
    element.classList.add('col')
    element.classList.add('product-card')
    let cardShadow = document.createElement('div');
    cardShadow.classList.add('card')
    cardShadow.classList.add('shadow-sm')

    let thumbnailImg= document.createElement("img")
    thumbnailImg.src="/Content/Resources/product-1.png"
    thumbnailImg.classList.add('bd-placeholder-img')
    thumbnailImg.classList.add('card-img-top')
    thumbnailImg.setAttribute('width' , "100%")
    thumbnailImg.setAttribute('height',"225")


    let cardBody = document.createElement("div")
    cardBody.classList.add("card-body")
    let cardDescription = document.createElement('p')
    cardDescription.classList.add("card-text")
    cardDescription.innerText = product.Description 
    let cardDescriptionContainer = document.createElement("div")
    cardDescriptionContainer.classList.add("d-flex")
    cardDescriptionContainer.classList.add('justify-content-between')
    cardDescriptionContainer.classList.add("align-items-center")
    let cardButtons = document.createElement("div")
    cardButtons.classList.add("btn-group")


    let buttonAddToCart= document.createElement('button')
    buttonAddToCart.type = "button"
    buttonAddToCart.classList.add("addtocartbutton")
    buttonAddToCart.classList.add("btn")
    buttonAddToCart.classList.add('btn-sm')
    buttonAddToCart.classList.add("btn-outline-secondary")
    buttonAddToCart.innerText = "Add To Cart"
    buttonAddToCart.onclick = function() {
        let trekkinguser = JSON.parse(localStorage.getItem('trekkinguser'));
        if(!trekkinguser) {
            window.location.href = "/user/login" 
        }
        else{
            console.log(order)
            $.ajax({
                type:"POST",
                url:"http://localhost:5000/api/order-items",
                data:{ OrderId:order.OrderId, ProductId:product.ProductId},
                success: function(data){
                    if(!data){
                        alert('Could not add to cart')
                    }
                    else{
                        alert('Item added to cart')
                    }
                }
            })
            
        }
        // console.log(product);
    }
    
    let small = document.createElement("small")
    small.classList.add("text-muted")
    small.innerText = product.Name

    cardButtons.appendChild(buttonAddToCart)

    let trekkinguser = JSON.parse(localStorage.getItem('trekkinguser'));
    if(trekkinguser?.Role==="admin") {
        let buttonDeleteProduct= document.createElement('button')
        buttonDeleteProduct.type = "button"
        buttonDeleteProduct.classList.add("deleteproductbutton")
        buttonDeleteProduct.classList.add("btn")
        buttonDeleteProduct.classList.add('btn-sm')
        buttonDeleteProduct.classList.add("btn-outline-secondary")
        buttonDeleteProduct.innerText = "Delete product"
        buttonDeleteProduct.onclick = function() {
            $.ajax({
                type:"DELETE",
                url:"http://localhost:5000/api/products/" + product.ProductId,
                success: function(data){
                    window.location.href = "/"
                }
            })
        }
        cardButtons.appendChild(buttonDeleteProduct)
    }
    

    cardDescriptionContainer.appendChild(cardButtons)
    cardDescriptionContainer.appendChild(small)
    cardBody.appendChild(cardDescription)
    cardBody.appendChild(cardDescriptionContainer)

    cardShadow.appendChild(thumbnailImg)
    cardShadow.appendChild(cardBody)
    element.appendChild(cardShadow)
    productTable.appendChild(element)
}

function createPagers(products){
    let numOfPagers = Math.ceil(products.length/6)
    let pagerSection = document.getElementById('pager-section')
    
    
    let liLeft = document.createElement('li')
    liLeft.classList.add('page-item')
    let aLeft = document.createElement("a")
    aLeft.classList.add('page-link')
    aLeft.innerText = "Previous"
    aLeft.onclick = function (){
        if(currentPage != 1) {
            currentPage--
            deleteProductCards()
            createProductCards(products.slice((currentPage-1)*6, (currentPage)*6))
            setPagerActive(currentPage)
        }
    }
    liLeft.appendChild(aLeft)
    pagerSection.appendChild(liLeft)
    
    for(let i = 1; i <= numOfPagers; i++ ){
        let li = document.createElement('li')
        li.classList.add('page-item')
        let a = document.createElement("a")
        a.classList.add('page-link')
        a.innerText = i
        a.onclick = function (){
            currentPage = i
            deleteProductCards()
            createProductCards(products.slice((currentPage-1)*6, (currentPage)*6))
            setPagerActive(currentPage)
        }
        li.appendChild(a)
        pagerSection.appendChild(li)
    }
    
    let liRight = document.createElement('li')
    liRight.classList.add('page-item')
    let aRight = document.createElement("a")
    aRight.classList.add('page-link')
    aRight.innerText = "Next"
    aRight.onclick = function (){
        if(currentPage != numOfPagers) {
            currentPage++
            deleteProductCards()
            createProductCards(products.slice((currentPage-1)*6, (currentPage)*6))
            setPagerActive(currentPage)
        }
    }
    liRight.appendChild(aRight)
    pagerSection.appendChild(liRight)
}

function deletePagers(){
    let pagerLinks = document.getElementsByClassName("page-item");
    for(var i = pagerLinks.length - 1; i >= 0; i--) {
        pagerLinks[i].remove();
    }
}

function setPagerActive(linkNumber){
    let pagerLinks = document.getElementsByClassName("page-item");
    for(let i = 0; i < pagerLinks.length ; i++) {
        pagerLinks[i].classList.remove('active')
    }
    pagerLinks[linkNumber].classList.add('active')
}

function getSearchWord(){
    var inputs = document.getElementById("search-input-field");
    return inputs.value
}

function searchProducts(searchWord, products){
    return products.filter(products=>{
        console.log(products.Name)
        return products.Name.toLowerCase().includes(searchWord.toLowerCase())
    })
}

