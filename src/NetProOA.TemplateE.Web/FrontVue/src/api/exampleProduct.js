import {post,put,del,get} from '../utils/request';
import qs from 'qs';

const pageExampleProducts = (model) => {
    return post({
        url: '/templatee/exampleProducts/pages',
        params: model
    });
};

const saveExampleProduct = (model) => {
	if(model.id==''||model.id==0||model.id==null||model.id==undefined){
		return post({
		    url: '/templatee/exampleProducts',
		    params: model
		});
	}
    else{
		return put({
			url: '/templatee/exampleProducts',
			params: model
		});
	}
};

const getExampleProduct = (id) => {
    return get({
        url: `/templatee/exampleProducts/${id}`,
        params:{}
    });
};

const deleteExampleProducts = (ids) => {
    return del({
        url: '/templatee/exampleProducts',
        params: {ids:ids}
    });
};
export {pageExampleProducts,saveExampleProduct,getExampleProduct,deleteExampleProducts}
 
