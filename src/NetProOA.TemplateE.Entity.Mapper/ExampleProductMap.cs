using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetProOA.Framework.Domain.Core.EF.Extension;
using NetProOA.TemplateE.Domain.AggregateModels.ExampleProductAggregate;

namespace NetProOA.TemplateE.Entity.Mapper
{
    public  class ExampleProductMap : EntityMappingConfiguration<ExampleProduct>
    {
        public override void Map(EntityTypeBuilder<ExampleProduct> t)
        {
            t.ToTable("ExampleProducts").ToTable(p=>p.HasComment("ʾ����"));
            t.Property(p => p.RowVersion).IsRowVersion().HasComment("�汾��");

            t.HasKey(p => p.Id);
            t.Property(p => p.Id).HasMaxLength(50).IsRequired().HasComment("����ID");
            t.Property(p => p.Name).HasMaxLength(50).IsRequired().HasComment("����");
            t.Property(p => p.Price).AsDecimal(18,4).IsRequired().HasComment("�۸�");

            #region ϵͳ�ֶ�
            t.Property(p => p.CreateEmplId).HasMaxLength(50).IsRequired().HasDefaultValue(string.Empty).HasComment("������ID");
            t.Property(p => p.CreateEmplName).HasMaxLength(50).IsRequired().HasDefaultValue(string.Empty).HasComment("������"); 
            t.Property(p => p.CreateDeptId).HasMaxLength(50).IsRequired().HasDefaultValue(string.Empty).HasComment("�����˲���ID");
            t.Property(p => p.CreateDeptName).HasMaxLength(50).IsRequired().HasDefaultValue(string.Empty).HasComment("�����˲���");
            t.Property(p => p.LastModifyEmplId).HasMaxLength(50).IsRequired().HasDefaultValue(string.Empty).HasComment("����޸���ID");
            t.Property(p => p.LastModifyEmplName).HasMaxLength(50).IsRequired().HasDefaultValue(string.Empty).HasComment("����޸���");  
            t.Property(p => p.LastModifyDeptId).HasMaxLength(50).IsRequired().HasDefaultValue(string.Empty).HasComment("����޸��˲���ID"); 
            t.Property(p => p.LastModifyDeptName).HasMaxLength(50).IsRequired().HasDefaultValue(string.Empty).HasComment("����޸��˲���");

            t.Property(p => p.CreateTime).IsRequired().HasComment("����ʱ��");
            t.Property(p => p.UpdateTime).IsRequired().HasComment("�޸�ʱ��");
            #endregion

        }
    }
}

