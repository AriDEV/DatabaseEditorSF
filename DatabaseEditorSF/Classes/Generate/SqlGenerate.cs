﻿using System;
using System.Windows.Forms;
using System.IO;

namespace DatabaseEditorSF.Classes.Generate {
	public abstract class SqlGenerate {
		public string deleteFromDatabase(string table, string where, object value) {
			return $"DELETE FROM {table} WHERE {where} = '{value}';" + Environment.NewLine;
		}

		public string insertToDatabase(string table, string[] columns, object[] values) {
			if(columns.Length == values.Length) {
				string column = "", value = "";

				for(var i = 0; i < columns.Length; i++) {
					column += columns[i];
					column += (i == columns.Length - 1 ? "" : ", ");
				}

				for(var i = 0; i < values.Length; i++) {
					value += "'" + values[i].ToString() + "'";
					value += (i == values.Length - 1 ? ""  : ", ");
				}

				return $"INSERT INTO {table} ({column}) VALUES ({value});" + Environment.NewLine;
			}

			return null;
		}

		public string updateToDatabase(string table, string[] columns, object[] values, string idcolumn, string id) {
			if(columns.Length == values.Length) {
				string query = String.Format("UPDATE {0} SET ", table);

				for(var i = 0; i < columns.Length; i++) {
					query += String.Format("{0} = '{1}'", columns[i], values[i].ToString());
					query += (i == columns.Length - 1 ? String.Format(" WHERE {0} = '{1}';" + Environment.NewLine, idcolumn, id) : ", ");
				}

				return query;
			}

			return null;
		}

		public string insertOrUpdateToDatabase(string table, string[] columns, object[] values) {
			if(columns.Length == values.Length) {
				string column = "", value = "", update = "";

				for(var i = 0; i < columns.Length; i++) {
					column += columns[i];
					column += (i == columns.Length - 1 ? "" : ", ");
				}

				for(var i = 0; i < values.Length; i++) {
					value += "'" + values[i].ToString() + "'";
					value += (i == values.Length - 1 ? "" : ", ");
				}

				for(var i = 0; i < columns.Length; i++) {
					update += $"{columns[i]} = '{values[i].ToString()}'";
					update += (i == columns.Length - 1 ? "" : ", ");
				}

				return $"INSERT INTO {table} ({column}) VALUES ({value}) ON DUPLICATE KEY UPDATE {update};" + Environment.NewLine;
			}

			return null;
		}

		public static bool generateSqlFile(string fileName, string sql) {
			if(!string.IsNullOrEmpty(sql)) {
				var sfd = new SaveFileDialog();

				sfd.Title = "DatabaseEditorSF - Save Output Sql";
				sfd.FileName = fileName;
				sfd.Filter = "Structured Query Language (*.sql)|*.sql";

				string path;

				if(sfd.ShowDialog() == DialogResult.OK) {
					path = sfd.FileName;
				} else { return false; }

				StreamWriter sw = new StreamWriter(path);

				sw.WriteLine(sql);

				sw.Close();

				return true;
			}

			return false;
		}
	}
}
