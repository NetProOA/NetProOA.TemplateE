import {
	createRouter,
	createWebHashHistory
} from "vue-router";
import routers from "./routers";
import {
	getProfile
} from "../api/frameworkApi"
import {
	useUserStore
} from '../utils/store'

const routes = [
	...routers,
	{
		path: '/',
		redirect: '/ExampleProduct/Index'
	}
];

const router = createRouter({
	history: createWebHashHistory(),
	routes
});

router.beforeEach((to, from, next) => {
	 const userStore = useUserStore();
	    try {
	        if( globalCurrentPermissionObject!=undefined){
	            console.log('权限赋值成功');
	            userStore.setPermission(globalCurrentPermissionObject);
	        }
	    }catch{
	
	    }
	next();
});

export default router;