using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Api.Models;

public partial class EscuelaContext : DbContext
{
    public EscuelaContext()
    {
    }

    public EscuelaContext(DbContextOptions<EscuelaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioRol> UsuarioRols { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=10.120.1.50;Database=escuela;Username=administrador;Password=Pass123!");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Idrol).HasName("rol_pkey");

            entity.ToTable("rol");

            entity.Property(e => e.Idrol).HasColumnName("idrol");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Habilitado)
                .HasDefaultValue(true)
                .HasColumnName("habilitado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("usuarios_pkey");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Email, "usuarios_email_key").IsUnique();

            entity.HasIndex(e => e.Usuario1, "usuarios_nombre_usuario_key").IsUnique();

            entity.Property(e => e.Idusuario)
                .HasDefaultValueSql("nextval('usuarios_id_seq'::regclass)")
                .HasColumnName("idusuario");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(255)
                .HasColumnName("contraseña");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Habilitado)
                .HasDefaultValue(true)
                .HasColumnName("habilitado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(50)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<UsuarioRol>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("usuario_rol");

            entity.Property(e => e.Idrol).HasColumnName("idrol");
            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Idusuariorol).HasColumnName("idusuariorol");

            entity.HasOne(d => d.IdrolNavigation).WithMany()
                .HasForeignKey(d => d.Idrol)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_rol");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany()
                .HasForeignKey(d => d.Idusuario)
                .HasConstraintName("usuario_roles_usuario_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
