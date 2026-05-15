import {post,put,del,get} from '../utils/frameworkRequest';
import qs from 'qs';
const getProfile = () => {
    return get({
        url: `/identity/employees/vueProfile`,
        params:{}
    });
};
export {getProfile}