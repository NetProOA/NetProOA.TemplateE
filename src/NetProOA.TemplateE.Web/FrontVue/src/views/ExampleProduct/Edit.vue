<template>
	<div>
		<el-dialog v-model="visible" title="编辑" @close="cancelCallback">
			<el-form ref="editForm" :model="editForm" :rules="rules" status-icon label-width="90px">
				<el-row>
					<el-col :span="12"> <el-form-item label="名称" prop="name">
							<el-input v-model="editForm.name"></el-input>
						</el-form-item> </el-col>
					<el-col :span="12"><el-form-item label="价格" prop="price">
							<el-input-number v-model="editForm.price" :min="1" style="width: 100%;" />
						</el-form-item> </el-col>
				</el-row>

				<el-row>
					<el-col :span="12"><el-form-item label="采购时间" prop="procurementTime">
							<el-date-picker v-model="editForm.procurementTime" type="date" format="YYYY-MM-DD"
								value-format="YYYY-MM-DD" placeholder="" style="width: 100%;" />
						</el-form-item> </el-col>
					<el-col :span="12"><el-form-item label="采购类型" prop="procurementType">
							<el-select v-model="editForm.procurementType" style="width: 100%;" clearable placeholder="">
								<el-option v-for="item in procurementTypeOptions" :key="item.value" :label="item.label" :value="item.value" />
							</el-select>
						</el-form-item> </el-col>
				</el-row>
			</el-form>
			<template #footer>
				<span class="dialog-footer">
					<el-button type="primary" @click="save" :loading="saveBtnLoading">保 存</el-button>
					<el-button @click="cancelCallback">关 闭</el-button>
				</span>
			</template>
		</el-dialog>
	</div>
</template>

<script>
	import {
		defineComponent,
		ref
	} from "vue";
	import {
		ElLoading,
		ElMessage,
		ElMessageBox
	} from "element-plus";
	import {
		saveExampleProduct,
		getExampleProduct
	} from "../../api/ExampleProduct.js";

	export default {
		components: {},
		name: 'ExampleProductEdit',
		props: {
			saveCallback: {
				type: Function,
				required: true
			},
			cancelCallback: {
				type: Function,
				required: true
			}
		},
		data() {
			return {
				editForm: {},
				rules: {},
				saveBtnLoading: false,
				visible:true,
				procurementTypeOptions: [{
					value: 1,
					label: '自主采购',
				}, {
					value: 2,
					label: '推荐采购'
				}]
			}
		},
		methods: {
			show(id) {
				var $this = this;
				if (!!id) {
					getExampleProduct(id).then((res) => {
						$this.editForm = res.data;
					});
				} else {
					$this.editForm.id = '';
				}
				this.visible=true;
				this.$nextTick(function() {
					this.$refs.editForm.resetFields();
				});
			},
			save() {
				this.$refs.editForm.validate((valid, fields) => {
					if (valid) {
						this.saveBtnLoading = true;
						saveExampleProduct(this.editForm).then((res) => {
							this.saveBtnLoading = false;
							ElMessage.success('保存成功');
							this.saveCallback();
						});
					} else {
						console.log("校验失败");
					}
				});
			}
		},
		mounted() {
			this.rules = {
				name: [{
					required: true,
					message: "名称必填",
					trigger: "blur",
				}, ],
			};
		}
	};
</script>

<style scoped>
</style>