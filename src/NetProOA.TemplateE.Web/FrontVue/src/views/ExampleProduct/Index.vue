<template>
	<div class="app-container">
		<div class="filter-container">
			<el-form ref="filterForm" :model="queryParam">
				<el-space wrap>
					<el-form-item label="名称" prop="name">
						<el-input v-model="queryParam.name" placeholder="请输入名称" class="filter-item"
							style="width: 110px;" />
					</el-form-item>
				</el-space>
				<el-space wrap>
					<el-form-item label="价格">
						<el-input-number v-model="queryParam.priceMin" :min="1" style="width: 100px;" />
						~
						<el-input-number v-model="queryParam.priceMax" :min="1" style="width: 100px;" />
					</el-form-item>
				</el-space>
				<el-space wrap>
					<el-form-item label="采购时间">
						<el-date-picker v-model="queryParam.procurementTimeMin" type="date" placeholder=""
							format="YYYY-MM-DD" value-format="YYYY-MM-DD" style="width: 125px;" />
						~
						<el-date-picker v-model="queryParam.procurementTimeMax" type="date" placeholder=""
							format="YYYY-MM-DD" value-format="YYYY-MM-DD" style="width: 125px;" />
					</el-form-item>
				</el-space>
				<el-space wrap>
					<el-form-item label="采购类型" prop="procurementType">
						<el-select v-model="queryParam.procurementType" clearable placeholder="" style="width: 100px;">
							<el-option v-for="item in procurementTypeOptions" :key="item.value" :label="item.label"
								:value="item.value" />
						</el-select>
					</el-form-item>
				</el-space>
				<el-button @click="doSearch">
					<el-icon>
						<Search />
					</el-icon>
					查询
				</el-button>
				<el-button @click="resetSearch">
					<el-icon>
						<Refresh />
					</el-icon>
					清空
				</el-button>
				<el-button type="primary" @click="showEditDlg()" v-show="permission.Allow_Add_模版本管理">
					<el-icon>
						<Plus />
					</el-icon>新增
				</el-button>

				<el-button type="danger" @click="deleteChoiced" :loading="deleteBtnLoading" v-show="permission.Allow_Delete_模版本管理" v-if="showDelteBtn">
					<el-icon>
						<Delete />
					</el-icon>删除
				</el-button>
			</el-form>
		</div>

		<div style="height: calc(100% - 85px); " class="body-container">
			<vxe-table stripe ref="refTable" height="100%" :data="pageData.datas"
				:row-config="{isCurrent: true, isHover: true}" :checkbox-config="{ highlight: true}"
				:column-config="{ resizable: true }" :loading="tableLoading" @checkbox-change="checkboxChangeHandler"
				@checkbox-all="selectAllChangeHandler">
				<vxe-column v-for="col in columns" :field="col.field" :title="col.title" :width="col.width"
					:type="col.type" :resizable="col.resizable" :formatter="col.formatter">
					<template v-if="col.field == 'name'" #default="{ row }">
					  <a type="primary" @click="showEditDlg(row.id)"> {{row[col.field] }}</a>
					</template>
					<template v-if="col.field == 'procurementType'" #default="{ row }">
						{{formatProcurementType(row)}}
					</template>
				</vxe-column>
			</vxe-table>
			<vxe-pager v-model:current-page="pageData.currentPage" v-model:page-size="pageData.pageSize"
				:total="pageData.total" @page-change="pageChange">
			</vxe-pager>
		</div>

		<ExampleProductEdit ref="example_product_edit" :id="editFormId" v-show="editFormVisible"
			:saveCallback="saveCallback" :cancelCallback="cancelCallback"></ExampleProductEdit>
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
		pageExampleProducts,
		deleteExampleProducts
	} from "../../api/ExampleProduct.js";
	import ExampleProductEdit from './Edit.vue';
	import { useUserStore } from '../../utils/store';
	export default {
		components: {
			ExampleProductEdit
		},
		data() {
			return {
				permission:useUserStore().permission,
				queryParam: {
					pageNumber: 1,
					pageSize: 50,
					isAscending: false
				},
				columns: [],
				pageData: {
					currentPage: 1,
					pageSize: 50,
					total: 0,
					datas: []
				},
				editFormId: '',
				editFormVisible: false,
				rules: {},
				showDelteBtn: false,
				deleteBtnLoading: false,
				tableLoading: false,
				saveBtnLoading: false,
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
			doSearch() {
				var $this = this;
				this.tableLoading = true;
				pageExampleProducts(this.queryParam).then((res) => {
					if (res.succeed) {
						$this.pageData.datas = res.data;
						$this.pageData.total = res.totalCount;
					}
					$this.tableLoading = false;
				});
			},
			resetSearch() {
				this.$refs.filterForm.resetFields();
				this.queryParam.pageNumber = 1;
				this.queryParam.priceMin = null;
				this.queryParam.priceMax = null;
				this.queryParam.procurementTimeMin = null;
				this.queryParam.procurementTimeMax = null;
				this.pageData.currentPage = 1;
				this.doSearch();
			},
			formatProcurementType(row) {
				if (row.procurementType == 1) return '自主采购';
				if (row.procurementType == 2) return '推荐采购';
			},
			showEditDlg(id) {
				this.editFormVisible = true;
				this.$refs.example_product_edit.show(id);
			},
			saveCallback() {
				this.queryParam.pageNumber = 1;
				this.pageData.currentPage = 1;
				this.doSearch();
				this.editFormVisible = false;
			},
			cancelCallback() {
				this.editFormVisible = false;
			},
			checkboxChangeHandler() {
				var selectRecords = this.$refs.refTable.getCheckboxRecords();
				if (selectRecords.length > 0) {
					this.showDelteBtn = true;
				} else {
					this.showDelteBtn = false;
				}
			},
			selectAllChangeHandler() {
				this.checkboxChangeHandler();
			},
			deleteChoiced() {
				var selectRecords = this.$refs.refTable.getCheckboxRecords();
				var ids = selectRecords.map((p) => p.id);
				this.deleteBtnLoading = true;

				ElMessageBox.confirm(
						'确认删除?',
						'Warning', {
							confirmButtonText: '确认',
							cancelButtonText: '取消',
							type: 'warning',
						}
					)
					.then(() => {
						deleteExampleProducts(ids).then((res) => {
							this.pageData.currentPage = 1;
							this.queryParam.pageNumber = 1;
							this.doSearch();
							this.deleteBtnLoading = false;
							this.showDelteBtn = false;
							ElMessage.success('删除成功');
						});
					})
					.catch(() => {
						this.deleteBtnLoading = false;
					})
			},
			pageChange({
				currentPage,
				pageSize
			}) {
				this.queryParam.pageNumber = currentPage;
				this.queryParam.pageSize = pageSize;
				this.doSearch();
			},
		},
		mounted() {
			this.columns = [{
					field: "id",
					type: "checkbox",
					width: 50,
					resizable: false
				},
				{
					field: "name",
					title: "名称"
				},
				{
					field: "price",
					title: "价格"
				},
				{
					field: "procurementTime",
					title: "采购时间",
					formatter: "formatShortDate"
				},
				{
					field: "procurementType",
					title: "采购类型"
				},
				{
					field: "createTime",
					title: "创建时间",
					formatter: "formatDate"
				},
			];
			this.doSearch();
		},
	};
</script>
<style scoped>
</style>