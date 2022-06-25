﻿var currentPage = 1
var foundProducts = []

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
    cardDescription.innerText = "This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer"
    let cardDescriptionContainer = document.createElement("div")
    cardDescriptionContainer.classList.add("d-flex")
    cardDescriptionContainer.classList.add('justify-content-between')
    cardDescriptionContainer.classList.add("align-items-center")
    let cardButtons = document.createElement("div")
    cardButtons.classList.add("btn-group")
    let buttonView = document.createElement('button')
    buttonView.type = "button"
    buttonView.classList.add("btn")
    buttonView.classList.add('btn-sm')
    buttonView.classList.add("btn-outline-secondary")

    buttonView.innerText = "View"
    let small = document.createElement("small")
    small.classList.add("text-muted")
    small.innerText = "TEST TEST"

    cardButtons.appendChild(buttonView)
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
    console.log(products)
    return products.filter(products=>{
        return products.Name.toLowerCase().includes(searchWord.toLowerCase())
    })
}
