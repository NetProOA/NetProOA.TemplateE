import { defineStore } from 'pinia'
export const useUserStore = defineStore('user', {
  state: () => ({
    permission: { Allow_Update_模版本管理:true,Allow_Delete_模版本管理:true,Allow_Add_模版本管理:true,Allow_View_模版本管理:true}
  }),
  actions: {
    setPermission(permission) {
      this.permission = permission
    }
  },
})