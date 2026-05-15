import ElementPlus from 'element-plus'
import * as ElementPlusIconsVue from '@element-plus/icons-vue'
import 'element-plus/dist/index.css'
import localeZH from 'element-plus/es/locale/lang/zh-cn'
import localeEN from 'element-plus/es/locale/lang/en'

import { createI18n } from 'vue-i18n'
import messages from '../utils/i18n'
const i18n = createI18n({
  locale: localeZH.name,
  fallbackLocale: localeEN.name,
  messages,
})

export default (app) => {
  app.use(ElementPlus, {  size: 'default',locale:localeZH});
  for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
    app.component(key, component)
  }
  app.use(i18n);
}
