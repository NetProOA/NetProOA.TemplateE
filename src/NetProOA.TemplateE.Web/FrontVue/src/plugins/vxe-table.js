import XEUtils from 'xe-utils'
import VXETable from 'vxe-table'
import 'vxe-table/lib/style.css'
VXETable.setup({
	size: "small", // 全局尺寸
	zIndex: 999, // 全局 zIndex 起始值，如果项目的的 z-index 样式值过大时就需要跟随设置更大，避免被遮挡
	version: 0, // 版本号，对于某些带数据缓存的功能有用到，上升版本号可以用于重置数据
	loadingText: "暂无数据", // 自定义loading提示内容，如果为null则不显示文本
	table: {
		showHeader: true,
		keepSource: false,
		showOverflow: true,
		showHeaderOverflow: true,
		showFooterOverflow: true,
		size: "small",
		autoResize: false,
		stripe: true,
		border: false,
		round: false,
		emptyText: '暂无数据',
		rowConfig: {
			isHover: true
		}
	},

	pager: {
		size: "small",
		autoHidden: false,
		perfect: true,
		pageSize: 50,
		pagerCount: 7,
		pageSizes: [50, 100, 200],
		layouts: ['PrevJump', 'PrevPage', 'Jump', 'PageCount', 'NextPage', 'NextJump', 'Sizes', 'Total']
	},
	// button: {
	//   size: null,
	//   transfer: false
	// },
	// radio: {
	//   size: null
	// },
	// checkbox: {
	//   size: null
	// },
	// switch: {
	//   size: null
	// },

})


// 自定义全局的格式化处理函数
VXETable.formats.mixin({
	// 格式化性别
	formatSex({ cellValue }) {
		return cellValue ? (cellValue === '1' ? '男' : '女') : ''
	},
	// 格式化下拉选项
	formatSelect({ cellValue }, list) {
		const item = list.find(item => item.value === cellValue)
		return item ? item.label : ''
	},
	// 格式化日期，默认 yyyy-MM-dd HH:mm:ss
	formatDate({ cellValue }, format) {
		return XEUtils.toDateString(cellValue, format || 'yyyy-MM-dd HH:mm:ss')
	},
	// 格式化日期，默认 yyyy-MM-dd
	formatShortDate({ cellValue }) {
		return XEUtils.toDateString(cellValue,  'yyyy-MM-dd')
	},
	// 四舍五入金额，每隔3位逗号分隔，默认2位数
	formatAmount({ cellValue }, digits = 2) {
		return XEUtils.commafy(Number(cellValue), { digits })
	},
	// 格式化银行卡，默认每4位空格隔开
	formatBankcard({ cellValue }) {
		return XEUtils.commafy(XEUtils.toValueString(cellValue), { spaceNumber: 4, separator: ' ' })
	},
	// 四舍五入,默认两位数
	formatFixedNumber({ cellValue }, digits = 2) {
		return XEUtils.toFixed(XEUtils.round(cellValue, digits), digits)
	},
	// 向下舍入,默认两位数
	formatCutNumber({ cellValue }, digits = 2) {
		return XEUtils.toFixed(XEUtils.floor(cellValue, digits), digits)
	}
})

export default (app) => {
	app.use(VXETable)
}
