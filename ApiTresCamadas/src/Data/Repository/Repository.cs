﻿using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
	public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new() //new para usar no remover
	{
		protected readonly MeuDbContext Db;
		protected readonly DbSet<TEntity> DbSet;//classe do EF para realizar leitura e escrita em uma tabela


		protected Repository(MeuDbContext db) 
		{
			Db = db;
			DbSet = db.Set<TEntity>();
		}

		public virtual async Task<TEntity> ObterPorId(Guid id)
		{
			return await DbSet.FindAsync(id);
		}

		public virtual async Task<List<TEntity>> ObterTodos()
		{
			return await DbSet.ToListAsync();
		}

		public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
		{
			return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
		}

		public virtual void Adicionar(TEntity entity)
		{
			DbSet.Add(entity);
		}

		public virtual void Atualizar(TEntity entity)
		{
			DbSet.Update(entity);
		}

		public virtual void Remover(Guid id)
		{
			DbSet.Remove(new TEntity { Id = id});
		}

		public async Task<int> SaveChanges()
		{
			return await Db.SaveChangesAsync();
		}

		public void Dispose() 
		{
			Db.Dispose();
		}
	}
}
