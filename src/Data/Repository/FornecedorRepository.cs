﻿using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
	public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
	{
		public FornecedorRepository(MeuDbContext context) : base(context)
		{

		}

		public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
		{
			return await Db.Fornecedores.AsNoTracking()
				.Include(c => c.Endereco)
				.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async  Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
		{
			return await Db.Fornecedores.AsNoTracking()
				.Include(c => c.Produtos)
				.Include(c => c.Endereco)
				.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
		{
			return await Db.Enderecos.AsNoTracking()
				.FirstOrDefaultAsync(f => f.FornecedorId == fornecedorId);
		}

		public async Task RemoverEnderecoFornecedor(Endereco endereco)
		{
			Db.Enderecos.Remove(endereco);
			await SaveChanges();
		}
	}
}
