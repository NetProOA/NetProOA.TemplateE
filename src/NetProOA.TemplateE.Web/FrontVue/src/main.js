import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import router from './router'

import installElementPlus from './plugins/element'
import installVxeTable from './plugins/vxe-table'
import { useUserStore } from './utils/store'

/*import VxeUI from 'vxe-pc-ui'*/
/*import 'vxe-pc-ui/lib/style.css'*/

/*import VxeUITable from 'vxe-table'*/
/*import 'vxe-table/lib/style.css'*/

import './assets/css/icon.css'
import './assets/css/theme.scss'
const app = createApp(App)
installElementPlus(app)
installVxeTable(app)
/*app.use(VxeUI);*/
/*app.use(VxeUITable);*/
app.use(createPinia());
app.provide('userStore', useUserStore());
app.use(router);
app.mount('#app');
