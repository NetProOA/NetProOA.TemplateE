

let exampleProduct = [
   {
    path: '/ExampleProduct/Index',
    name: 'ExampleProduct_Index',
	meta: {
	    title: 'ExampleProduct'
	},
    component:  () => import('../views/ExampleProduct/Index.vue' )
  } ]

export default exampleProduct

