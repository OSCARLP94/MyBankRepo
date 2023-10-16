const Initial_State={
    products : []
};

export function ProductReducer(state = Initial_State, action) {
    switch (action.type) {
      case "FULL_PRODUCTS":
        return {
          products: action.products,
        };
      case "UPDATE_PRODUCT":
          var newState= state.products.map((item)=>{
              if(item.id  === action.product.id)
                  return action.product;
              else 
                  return item;
          });
  
          return{
              products : newState
          };
      default:
        return state;
    }
  }