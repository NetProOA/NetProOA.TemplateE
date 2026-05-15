# NetProOA.TemplateE.Api

基于 **NetProOA.Framework** 构建的企业级微服务 API 模板项目，提供开箱即用的服务端开发体验。

## 🌐 官方网站

访问项目官网获取更多信息：[https://netprooa.com/](https://netprooa.com/)

## ✨ 功能特点

- **架构设计**：基于 .NET 8 构建的现代化微服务架构
- **服务治理**：集成 Consul/Nacos 服务发现与配置管理
- **统一认证**：内置 JWT 认证授权机制
- **API 文档**：自动生成 Swagger API 文档
- **分布式追踪**：集成 Jaeger/Zipkin 分布式链路追踪
- **健康检查**：内置健康检查端点
- **日志管理**：集成 NLog 日志框架
- **配置管理**：支持 Apollo/Nacos/Consul 多种配置中心

## 🛠️ 技术栈

| 分类 | 技术 | 版本 |
| :--- | :--- | :--- |
| 框架 | ASP.NET Core | 8.0 |
| 数据库 | MySQL / SQL Server / MongoDB | - |
| 服务发现 | Consul / Nacos | - |
| 配置中心 | Apollo / Nacos / Consul | - |
| 消息队列 | RabbitMQ | - |
| 缓存 | Redis | - |
| 分布式追踪 | Jaeger / Zipkin | - |
| 日志 | NLog | - |

## 🚀 快速开始

### 环境要求

- .NET 8 SDK
- MySQL 8.0+ / SQL Server 2019+ / MongoDB 4.0+
- Redis 6.0+（可选）
- Consul / Nacos（可选）

### 运行项目

```bash
# 克隆项目
git clone <repository-url>
cd src/NetProOA.TemplateE.Api

# 配置数据库连接（修改 Config/appsettings.json）

# 运行项目
dotnet run
```

### 访问地址

- **API 文档**: http://localhost:5000/swagger
- **健康检查**: http://localhost:5000/health
- **首页**: http://localhost:5000

## ⚙️ 配置说明

配置文件位于 `Config/` 目录下，支持多环境配置：

| 文件 | 说明 |
| :--- | :--- |
| `appsettings.json` | 基础配置 |
| `appsettings.Development.json` | 开发环境配置 |
| `appsettings.Production.json` | 生产环境配置 |
| `nacos.{Environment}.json` | Nacos 配置 |
| `apollo.{Environment}.json` | Apollo 配置 |
| `consul.{Environment}.json` | Consul 配置 |

## 📦 项目结构

```
src/
├── NetProOA.TemplateE.Api/           # API 主项目
│   ├── Config/                       # 配置文件
│   ├── Controllers/                  # API 控制器
│   ├── Deploy/                       # 部署配置
│   │   ├── nginx/                    # Nginx 配置
│   │   └── k8s.yml                   # Kubernetes 部署
│   ├── Program.cs                    # 入口文件
│   └── Startup.cs                    # 启动配置
├── NetProOA.TemplateE.ClientSdk/     # 客户端 SDK
├── NetProOA.TemplateE.Api.Test/      # 单元测试
├── NetProOA.TemplateE.T4/            # T4 代码生成器
│   ├── EntityHelper.ttinclude        # 实体定义配置
│   ├── EntityIntGenerate.tt          # Int 主键代码生成
│   ├── EntityGuidGenerate.tt         # Guid 主键代码生成
│   └── VueGenerate.tt                # Vue 前端代码生成
└── NetProOA.TemplateE.Api.sln        # 解决方案文件
```

## 🔧 T4 代码生成器

项目内置 T4 模板代码生成器，可快速生成完整的 CRUD 代码结构。

### 使用方法

1. 打开 `NetProOA.TemplateE.T4/EntityHelper.ttinclude` 文件
2. 在 `GetEntities()` 方法中添加实体名称：
   ```csharp
   list.Add(new Entity("YourEntityName"));
   ```
3. 右键点击 `EntityIntGenerate.tt`（Int 主键）或 `EntityGuidGenerate.tt`（Guid 主键）
4. 选择 **调试 T4 模板**

### 生成的文件清单

| 文件类型 | 路径 | 说明 |
| :--- | :--- | :--- |
| Repository 接口 | `Domain/Repositories/I{Entity}Repository.cs` | 仓储接口 |
| ReadOnlyRepository 接口 | `Domain/Repositories/ReadOnly/I{Entity}ReadOnlyRepository.cs` | 只读仓储接口 |
| Repository 实现 | `Repository/{Entity}Repository.cs` | 仓储实现 |
| ReadOnlyRepository 实现 | `Repository/ReadOnly/{Entity}ReadOnlyRepository.cs` | 只读仓储实现 |
| DTO | `Application/DataTransfer/{Entity}/{Entity}Dto.cs` | 数据传输对象 |
| 查询接口 | `Application/Queries/I{Entity}Queries.cs` | 查询接口 |
| 查询实现 | `Application/Queries/Impl/{Entity}Queries.cs` | 查询实现 |
| 查询条件 | `Application/Queries/Criteria/{Entity}QueryCriteria.cs` | 查询条件 |
| Create 命令 | `Application/Commands/{Entity}/Create{Entity}Command.cs` | 创建命令 |
| Modify 命令 | `Application/Commands/{Entity}/Modify{Entity}Command.cs` | 修改命令 |
| Delete 命令 | `Application/Commands/{Entity}/Delete{Entity}Command.cs` | 删除命令 |
| 命令验证器 | `Application/Commands/{Entity}/Validations/*.cs` | FluentValidation 验证器 |
| 命令处理器 | `Application/Commands/{Entity}/Handlers/*.cs` | MediatR 命令处理器 |
| Specification | `Domain/AggregateModels/{Entity}Aggregate/Specifications/*.cs` | 规格模式 |
| AutoMapper Profile | `Infrastructure.AutoMapper/{Entity}Profile.cs` | 映射配置 |
| Entity Mapping | `Entity.Mapper/{Entity}Map.cs` | EF Core 实体映射 |
| Controller | `Api/Controllers/{Entity}Controller.cs` | REST API 控制器 |

### Vue 前端代码生成

`VueGenerate.tt` 用于生成 Vue 前端代码，包括：

| 文件类型 | 路径 | 说明 |
| :--- | :--- | :--- |
| API 文件 | `FrontVue/src/api/{entity}.js` | 封装 REST API 调用 |
| 路由配置 | `FrontVue/src/router/{entity}Router.js` | 页面路由配置 |
| 列表页面 | `FrontVue/src/views/{Entity}/Index.vue` | 列表展示与分页 |
| 编辑页面 | `FrontVue/src/views/{Entity}/Edit.vue` | 新增/编辑表单 |

**生成的 Vue 组件特性：**
- 使用 Element Plus 组件库
- 支持分页查询、新增、编辑、删除操作
- 包含权限控制（基于用户权限显示按钮）
- 使用 vxe-table 进行表格展示
- 集成确认弹窗和加载状态

### AI 代码生成提示词

以下是可以发送给 AI 来生成类似代码的描述：

```
请帮我基于 NetProOA.Framework 框架生成一个完整的 CRUD 模块，实体名为 {EntityName}，主键类型为 int。

需要生成以下文件：

1. **仓储层**:
   - `I{EntityName}Repository.cs` - 继承 IRepository<{EntityName}, int>
   - `I{EntityName}ReadOnlyRepository.cs` - 继承 IReadOnlyRepository<{EntityName}, int>
   - `{EntityName}Repository.cs` - 继承 EFDBRepository<{EntityName}, int>
   - `{EntityName}ReadOnlyRepository.cs` - 继承 EFDBReadOnlyRepository<{EntityName}, int>

2. **应用层 - 查询**:
   - `{EntityName}Dto.cs` - 包含 Id 属性
   - `I{EntityName}Queries.cs` - 包含分页查询和单条查询方法
   - `{EntityName}Queries.cs` - 查询实现，注入只读仓储和映射器
   - `{EntityName}QueryCriteria.cs` - 继承 PagedQueryCriteria

3. **应用层 - 命令**:
   - `Create{EntityName}Command.cs` - 继承 CommandBase, IRequest<bool>
   - `Modify{EntityName}Command.cs` - 继承 CommandBase, IRequest<bool>，包含 Id 属性
   - `Delete{EntityName}Command.cs` - 继承 CommandBase, IRequest<bool>，包含 List<int> Ids 属性
   - 对应的 Validator 验证器类（FluentValidation）
   - 对应的 Handler 处理器类（MediatR）

4. **领域层**:
   - `Match{EntityName}ByIdsSpecification.cs` - 继承 Specification<{EntityName}>

5. **基础设施层**:
   - `{EntityName}Profile.cs` - AutoMapper 配置
   - `{EntityName}Map.cs` - EF Core 实体映射配置，映射到表 "{EntityName}s"

6. **API 层**:
   - `{EntityName}Controller.cs` - REST API 控制器，包含 CRUD 接口：
     - POST /api/{entityName}s/pages - 分页查询
     - POST /api/{entityName}s - 创建
     - PUT /api/{entityName}s - 修改
     - GET /api/{entityName}s/{id} - 详情
     - DELETE /api/{entityName}s - 删除

命名空间约定：
- 领域层: `SaaS.TemplateE.Domain.*`
- 应用层: `SaaS.TemplateE.Application.*`
- 仓储层: `SaaS.TemplateE.Repository.*`
- API 层: `SaaS.TemplateE.Web.Controllers`

使用依赖注入，遵循 CQRS 模式和领域驱动设计原则。
```

> **提示**: 将 `{EntityName}` 替换为实际的实体名称（如 `Order`、`Product` 等）。

更多框架信息请访问官方网站：[https://netprooa.com/](https://netprooa.com/)

## 🐳 Docker 部署

```bash
# 构建镜像
docker build -t netprooa-templatee-api -f NetProOA.TemplateE.Api.Dockerfile .

# 运行容器
docker run -p 5000:80 --name templatee-api netprooa-templatee-api
```

## ☸️ Kubernetes 部署

```bash
# 部署到 Kubernetes
kubectl apply -f Deploy/k8s.yml
```

## 📖 更多资源

- **官方文档**: [https://netprooa.com/docs](https://netprooa.com/docs)
- **API 文档**: [https://netprooa.com/api](https://netprooa.com/api)
- **示例项目**: [https://netprooa.com/examples](https://netprooa.com/examples)

## 📄 许可证

MIT License

---

**NetProOA.Framework** - 企业级微服务开发框架

[![NetProOA](https://img.shields.io/badge/NetProOA-Framework-blue)](https://netprooa.com/)
