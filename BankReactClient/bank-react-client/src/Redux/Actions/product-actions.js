export const ProductActions ={
    Full,
    Update
} 

function Full(products){
    return{
        type : 'FULL_PRODUCTS',
        products
    }
}

function Update(product){
    return {
        type : 'UPDATE_PRODUCT',
        product      
    }
}