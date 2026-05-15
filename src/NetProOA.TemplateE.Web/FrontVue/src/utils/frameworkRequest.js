import axios from "axios";
import {
	ElLoading,
	ElMessage
} from "element-plus";
import qs from 'qs';
import jwt_decode from "jwt-decode";
import moment from 'moment';

axios.defaults.timeout = 600000;
axios.defaults.headers.post["Content-Type"] = "application/json;charset=UTF-8";
axios.defaults.headers.put["Content-Type"] = "application/json;charset=UTF-8";
axios.defaults.headers.delete["Content-Type"] = "application/json;charset=UTF-8";
const base_url =import.meta.env.VITE_FrameWork_BASE_URL;

try{
	 axios.defaults.baseURL =base_url;
}catch(e)
{
}	

const service = axios.create({
	timeout: 600000,
});
		
service.interceptors.request.use(
	(config) => {
		return config;
	},
	(error) => {
		console.log(error);
		return Promise.reject();
	}
);

service.interceptors.response.use(
	(response) => {
		if (response.status === 200) {
			return response;
		} else {
			Promise.reject();
		}
	},
	(error) => {
		console.log(error);
		checkAuth(error);
		return Promise.reject();
	}
);

const checkAuth = (error) => {
	if (error.response) {
		switch (error.response.status) {
			case 401: {
				ElMessage.error({
					showClose: true,
					message: "登陆已过期",
					type: "error",
				});
				break;
			}
			case 400:
				error.message = "错误请求";
				break;
			case 401:
				error.message = "未授权，请重新登录";
				break;
			case 403:
				error.message = "拒绝访问";
				break;
			case 405:
				error.message = "请求方法未允许";
				break;
			case 408:
				error.message = "请求超时";
				break;
			case 500:
				error.message = "服务器端出错";
				break;
			case 501:
				error.message = "网络未实现";
				break;
			case 502:
				error.message = "网络错误";
				break;
			case 503:
				error.message = "服务不可用";
				break;
			case 504:
				error.message = "网络超时";
				break;
			case 505:
				error.message = "http版本不支持该请求";
				break;
			default:
				error.message = `连接错误${error.response.status}`;
		}
		ElMessage.error(error.message);
	} else {
		if (JSON.stringify(error).includes("timeout")) {}
		error.message = "连接服务器失败";
		ElMessage.error(error.message);
	}
	return Promise.resolve(error.response);
};

const getToken = () => {
	
	if(import.meta.env.VITE_Token!=''){
		return import.meta.env.VITE_Token;
	}
	
	if (localStorage.getItem("token") != undefined) {
		var tokenObj = JSON.parse(localStorage.getItem("token"));
		return `Bearer ${tokenObj.access_token}`;
	}
	return 'Bearer ';
};


const beforeAjax = () => {
	 return new Promise((resolve, reject) => {
	 	resolve('tokenuseful')
	 });
}

const post = ({
	url,
	params,
	config
}) => {
	return wrapPost({
		url,
		params,
		config
	});
};

const wrapPost = ({
	url,
	params,
	config
}) => {
	service.defaults.headers["Authorization"] = getToken();
	if (
		!(
			!!config &&
			config.headers["Content-Type"] ==
			"application/x-www-form-urlencoded;charset=UTF-8"
		)
	) {
		params = JSON.stringify(params);
	}
	return new Promise((resolve, reject) => {
		service.post(url, params, config).then((response) => {
			resolve(response.data);
		});
	});
}


const put = ({
	url,
	params,
	config
}) => {
	return wrapPut({
		url,
		params,
		config
	});
};

const wrapPut = ({
	url,
	params,
	config
}) => {
	service.defaults.headers["Authorization"] = getToken();
	if (
		!(
			!!config &&
			config.headers["Content-Type"] ==
			"application/x-www-form-urlencoded;charset=UTF-8"
		)
	) {
		params = JSON.stringify(params);
	}
	return new Promise((resolve, reject) => {
		service.put(url, params, config).then((response) => {
			resolve(response.data);
		});
	});
}



const del = ({
	url,
	params,
	config
}) => {
	return wrapDelete({
		url,
		params,
		config
	});
};

const wrapDelete = ({
	url,
	params,
	config
}) => {
	service.defaults.headers["Authorization"] = getToken();
	if (
		!(
			!!config &&
			config.headers["Content-Type"] ==
			"application/x-www-form-urlencoded;charset=UTF-8"
		)
	) {
		//params = JSON.stringify(params);
	}
	return new Promise((resolve, reject) => {
		service.delete(url,{data: params}, config).then((response) => {
			resolve(response.data);
		});
	});
}

//=true异步请求时会显示遮罩层,=字符串，异步请求时遮罩层显示当前字符串
const get = ({
	url,
	param,
	config
}) => {
	return beforeAjax().then(res => {
		if (res != 'tokenuseful') {
			console.log('刷新Token成功');
			localStorage.setItem("token", JSON.stringify(res));
		}
		return wrapGet({
			url,
			param,
			config
		});
	});
};

const wrapGet = ({
	url,
	param,
	config
}) => {
	service.defaults.headers["Authorization"] = getToken();
	return new Promise((resolve, reject) => {
		service.get(url, config).then((response) => {
			resolve(response.data);
		});
	});
};
export {
	post,
	get,
	put,
	del,
	service
};
