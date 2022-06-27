using ApiClientes.Infra.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientes.Infra.Data.Mappings
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            //nome da tabela do banco de dados
            builder.ToTable("CLIENTE");

            //mapear o campo chave primária
            builder.HasKey(e => e.IdCliente);

            //mapear cada campo da tabela
            builder.Property(e => e.IdCliente)
            .HasColumnName("IDCLIENTE");
            

            builder.Property(e => e.Nome)
            .HasColumnName("NOME")
            .HasMaxLength(100)
            .IsRequired();

            builder.Property(e => e.Email)
            .HasColumnName("EMAIL")
            .HasMaxLength(50)
            .IsRequired();

            builder.Property(e => e.Cpf)
            .HasColumnName("CPF")
            .HasMaxLength(15)
            .IsRequired();

            builder.Property(e => e.DataNascimento)
            .HasColumnName("DATANASCIMENTO")
            .IsRequired();
        }
    }
}    