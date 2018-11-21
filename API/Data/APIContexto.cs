using Find.Models;
using Microsoft.EntityFrameworkCore;

namespace Find.Data
{
    public class APIContexto:DbContext
    {
        // Estamos criando um construtor para a classe que assumirá o papel de contexto
        public APIContexto(DbContextOptions<APIContexto> options):base(options)
        {}

        /* Abaixo temos a criação dos DbSet. Esses elementos são a representação
        das tabelas de forma lógica, ou seja, armazenam os dados antes de ir para 
        as tabelas no banco.
        */
        public DbSet<Acesso> Acesso { get; set; }
        public DbSet<Aula> Aula { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<CursoProfessor> CursoProfessor { get; set; }
        public DbSet<Pagamento> Pagamento { get; set; }
        public DbSet<Pergunta> Pergunta { get; set; }
        public DbSet<Professor> Professor { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        
        protected override void OnModelCreating(ModelBuilder mb)
        {
            // Relação da tabela de Pagamentos
            mb.Entity<Pagamento>().ToTable("Pagamento").HasKey(pgt => pgt.Id);
            mb.Entity<Pagamento>().ToTable("Pagamento").HasOne<Usuario>(pgUs => pgUs.Usuario).WithMany(usP => usP.Pagamento).HasForeignKey(pgUs => pgUs.IdUsuario);

            // Relação da tabela de Usuario
            mb.Entity<Usuario>().ToTable("Usuario").ToTable("Usuario").HasKey(user => user.Id);

            // Relação da tabela de Acesso
            mb.Entity<Acesso>().ToTable("Acesso").HasKey(ac => ac.Id);
            mb.Entity<Acesso>().ToTable("Acesso").HasOne<Usuario>(acUs => acUs.Usuario).WithMany(usAc => usAc.Acesso).HasForeignKey(acUs => acUs.idUsuario);
            mb.Entity<Acesso>().ToTable("Acesso").HasOne<Curso>(acCur => acCur.Curso).WithMany(curAc => curAc.Acesso).HasForeignKey(acCur => acCur.idCurso);

            // Relação da tabela de Professores
            mb.Entity<Professor>().ToTable("Professor").HasKey(pro => pro.Id);

            // Relação da tabela CursoProfessor
            mb.Entity<CursoProfessor>().ToTable("CursoProfessor").HasOne<Professor>(curPro => curPro.Professor).WithMany(proCur => proCur.CursoProfessor).HasForeignKey(curPro => curPro.idProfessor);
            mb.Entity<CursoProfessor>().ToTable("CursoProfessor").HasOne<Curso>(cuPro => cuPro.Curso).WithMany(cur => cur.CursoProfessor ).HasForeignKey(cuPro => cuPro.idCurso);

            // Relação da tabela de Curso
            mb.Entity<Curso>().ToTable("Curso").HasKey(cur => cur.Id);

            // Relação da tabela de Aula
            mb.Entity<Aula>().ToTable("Aula").HasKey(au => au.Id);
            mb.Entity<Aula>().ToTable("Aula").HasOne<Curso>(auCur => auCur.Curso).WithMany(curAu => curAu.Aula).HasForeignKey(auCur => auCur.idCurso);

            // Relação da tabela de Pergunta
            mb.Entity<Pergunta>().ToTable("Pergunta").HasKey(pgt => pgt.Id);
            mb.Entity<Pergunta>().ToTable("Pergunta").HasOne<Usuario>(pgUs => pgUs.Usuario).WithMany(usPg => usPg.Pergunta).HasForeignKey(pgUs => pgUs.idUsuario);
            mb.Entity<Pergunta>().ToTable("Pergunta").HasOne<Aula>(pgAu => pgAu.Aula).WithMany(auPg => auPg.Pergunta).HasForeignKey(pgAu => pgAu.idAula);

            // Relação da tabela de Resposta
            mb.Entity<Resposta>().ToTable("Resposta").HasKey(res => res.Id);
            mb.Entity<Resposta>().ToTable("Resposta").HasOne<Pergunta>(resPer => resPer.Pergunta).WithMany(perRes => perRes.Resposta).HasForeignKey(resPer => resPer.idPergunta);
            mb.Entity<Resposta>().ToTable("Resposta").HasOne<Usuario>(resUs => resUs.Usuario).WithMany(usRes => usRes.Resposta).HasForeignKey(resUs => resUs.idUsuario);
            mb.Entity<Resposta>().ToTable("Resposta").HasOne<Professor>(resPro => resPro.Professor).WithMany(proRes => proRes.Resposta).HasForeignKey(resPro => resPro.idProfessor);
        }
    }
}