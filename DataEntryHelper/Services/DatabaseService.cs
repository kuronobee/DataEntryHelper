using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace DataEntryHelper.Services
{
    /// <summary>
    /// SQLiteデータベースの操作を行うサービスクラス
    /// </summary>
    public class DatabaseService
    {
        // データベースファイルのパス
        private readonly string _dbPath;
        // データベース接続文字列
        private readonly string _connectionString;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DatabaseService()
        {
            // データベースファイルの保存先ディレクトリ
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PatientData");
            Directory.CreateDirectory(directoryPath);

            // データベースファイルのパス
            _dbPath = Path.Combine(directoryPath, "PatientDatabase.db");

            // 接続文字列の設定
            _connectionString = $"Data Source={_dbPath};Version=3;";

            // データベースが存在しない場合は作成
            if (!File.Exists(_dbPath))
            {
                CreateDatabase();
            }
        }

        /// <summary>
        /// データベースとテーブルを作成
        /// </summary>
        private void CreateDatabase()
        {
            try
            {
                // データベースファイル作成
                SQLiteConnection.CreateFile(_dbPath);

                // 接続を開いてテーブル作成
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    // patientsテーブルの作成
                    string createTableSql = @"
                        CREATE TABLE patients (
                            Id TEXT PRIMARY KEY,
                            Gender TEXT,
                            Age TEXT,
                            Height TEXT,
                            Weight TEXT,
                            BSA TEXT,
                            BMI TEXT,
                            SystolicBP TEXT,
                            DiastolicBP TEXT,
                            HeartRate TEXT,
                            Rhythm TEXT,
                            Alcohol TEXT,
                            Smoking TEXT,
                            Hypertension TEXT,
                            Diabetes TEXT,
                            Dyslipidemia TEXT,
                            CKD TEXT,
                            Stroke TEXT,
                            HeartFailure TEXT,
                            VascularDisease TEXT,
                            CoronaryIschemia TEXT,
                            Cardiomyopathy TEXT,
                            Dementia TEXT,
                            Others TEXT,
                            AtrialFibrillationType TEXT,
                            AtrialFibrillationSymptoms TEXT,
                            Chads2Score TEXT,
                            Cha2ds2VascScore TEXT,
                            PatientJson TEXT,
                            CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
                            UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
                        );";

                    using (SQLiteCommand command = new SQLiteCommand(createTableSql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"データベース作成中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 患者データを保存
        /// </summary>
        /// <param name="patientData">保存する患者データ</param>
        /// <returns>保存成功したかどうか</returns>
        public bool SavePatientData(PatientData patientData)
        {
            try
            {
                // 患者IDが空の場合はエラー
                if (string.IsNullOrWhiteSpace(patientData.Id))
                {
                    MessageBox.Show("患者IDが入力されていません。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                // 既存データの確認
                bool patientExists = CheckPatientExists(patientData.Id);

                // JSON形式に変換
                string patientJson = JsonSerializer.Serialize(patientData, new JsonSerializerOptions { WriteIndented = true });

                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    string sql;
                    if (patientExists)
                    {
                        // UPDATE文
                        sql = @"
                            UPDATE patients SET
                                Gender = @Gender,
                                Age = @Age,
                                Height = @Height,
                                Weight = @Weight,
                                BSA = @BSA,
                                BMI = @BMI,
                                SystolicBP = @SystolicBP,
                                DiastolicBP = @DiastolicBP,
                                HeartRate = @HeartRate,
                                Rhythm = @Rhythm,
                                Alcohol = @Alcohol,
                                Smoking = @Smoking,
                                Hypertension = @Hypertension,
                                Diabetes = @Diabetes,
                                Dyslipidemia = @Dyslipidemia,
                                CKD = @CKD,
                                Stroke = @Stroke,
                                HeartFailure = @HeartFailure,
                                VascularDisease = @VascularDisease,
                                CoronaryIschemia = @CoronaryIschemia,
                                Cardiomyopathy = @Cardiomyopathy,
                                Dementia = @Dementia,
                                Others = @Others,
                                AtrialFibrillationType = @AtrialFibrillationType,
                                AtrialFibrillationSymptoms = @AtrialFibrillationSymptoms,
                                Chads2Score = @Chads2Score,
                                Cha2ds2VascScore = @Cha2ds2VascScore,
                                PatientJson = @PatientJson,
                                UpdatedAt = CURRENT_TIMESTAMP
                            WHERE Id = @Id;";
                    }
                    else
                    {
                        // INSERT文
                        sql = @"
                            INSERT INTO patients (
                                Id, Gender, Age, Height, Weight, BSA, BMI,
                                SystolicBP, DiastolicBP, HeartRate, Rhythm,
                                Alcohol, Smoking, Hypertension, Diabetes, Dyslipidemia,
                                CKD, Stroke, HeartFailure, VascularDisease, 
                                CoronaryIschemia, Cardiomyopathy, Dementia, Others,
                                AtrialFibrillationType, AtrialFibrillationSymptoms, 
                                Chads2Score, Cha2ds2VascScore, PatientJson
                            ) VALUES (
                                @Id, @Gender, @Age, @Height, @Weight, @BSA, @BMI,
                                @SystolicBP, @DiastolicBP, @HeartRate, @Rhythm,
                                @Alcohol, @Smoking, @Hypertension, @Diabetes, @Dyslipidemia,
                                @CKD, @Stroke, @HeartFailure, @VascularDisease,
                                @CoronaryIschemia, @Cardiomyopathy, @Dementia, @Others,
                                @AtrialFibrillationType, @AtrialFibrillationSymptoms,
                                @Chads2Score, @Cha2ds2VascScore, @PatientJson
                            );";
                    }

                    using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                    {
                        // パラメータ設定
                        command.Parameters.AddWithValue("@Id", patientData.Id);
                        command.Parameters.AddWithValue("@Gender", patientData.Gender ?? "");
                        command.Parameters.AddWithValue("@Age", patientData.Age ?? "");
                        command.Parameters.AddWithValue("@Height", patientData.Height ?? "");
                        command.Parameters.AddWithValue("@Weight", patientData.Weight ?? "");
                        command.Parameters.AddWithValue("@BSA", patientData.BSA ?? "");
                        command.Parameters.AddWithValue("@BMI", patientData.BMI ?? "");
                        command.Parameters.AddWithValue("@SystolicBP", patientData.SystolicBP ?? "");
                        command.Parameters.AddWithValue("@DiastolicBP", patientData.DiastolicBP ?? "");
                        command.Parameters.AddWithValue("@HeartRate", patientData.HeartRate ?? "");
                        command.Parameters.AddWithValue("@Rhythm", patientData.Rhythm ?? "");
                        command.Parameters.AddWithValue("@Alcohol", patientData.Alcohol ?? "");
                        command.Parameters.AddWithValue("@Smoking", patientData.Smoking ?? "");
                        command.Parameters.AddWithValue("@Hypertension", patientData.Hypertension ?? "");
                        command.Parameters.AddWithValue("@Diabetes", patientData.Diabetes ?? "");
                        command.Parameters.AddWithValue("@Dyslipidemia", patientData.Dyslipidemia ?? "");
                        command.Parameters.AddWithValue("@CKD", patientData.CKD ?? "");
                        command.Parameters.AddWithValue("@Stroke", patientData.Stroke ?? "");
                        command.Parameters.AddWithValue("@HeartFailure", patientData.HeartFailure ?? "");
                        command.Parameters.AddWithValue("@VascularDisease", patientData.VascularDisease ?? "");
                        command.Parameters.AddWithValue("@CoronaryIschemia", patientData.CoronaryIschemia ?? "");
                        command.Parameters.AddWithValue("@Cardiomyopathy", patientData.Cardiomyopathy ?? "");
                        command.Parameters.AddWithValue("@Dementia", patientData.Dementia ?? "");
                        command.Parameters.AddWithValue("@Others", patientData.Others ?? "");
                        command.Parameters.AddWithValue("@AtrialFibrillationType", patientData.AtrialFibrillationType ?? "");
                        command.Parameters.AddWithValue("@AtrialFibrillationSymptoms", patientData.AtrialFibrillationSymptoms ?? "");
                        command.Parameters.AddWithValue("@Chads2Score", patientData.Chads2Score ?? "");
                        command.Parameters.AddWithValue("@Cha2ds2VascScore", patientData.Cha2ds2VascScore ?? "");
                        command.Parameters.AddWithValue("@PatientJson", patientJson);

                        command.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"データ保存中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// 患者データが存在するかチェック
        /// </summary>
        /// <param name="patientId">患者ID</param>
        /// <returns>存在するかどうか</returns>
        private bool CheckPatientExists(string patientId)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    string sql = "SELECT COUNT(*) FROM patients WHERE Id = @Id;";
                    using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", patientId);
                        long count = (long)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"データ確認中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// 患者データを読み込む
        /// </summary>
        /// <param name="patientId">患者ID</param>
        /// <returns>患者データ（見つからない場合はnull）</returns>
        public PatientData LoadPatientData(string patientId)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    string sql = "SELECT PatientJson FROM patients WHERE Id = @Id;";
                    using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", patientId);

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            string patientJson = (string)result;
                            return JsonSerializer.Deserialize<PatientData>(patientJson);
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"データ読み込み中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        /// <summary>
        /// 患者IDリストを取得
        /// </summary>
        /// <returns>患者IDのリスト</returns>
        public List<PatientListItem> GetPatientList()
        {
            List<PatientListItem> patientList = new List<PatientListItem>();

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    string sql = @"
                        SELECT 
                            Id, 
                            Gender, 
                            Age, 
                            AtrialFibrillationType,
                            Hypertension,
                            Diabetes,
                            CreatedAt,
                            UpdatedAt
                        FROM patients 
                        ORDER BY UpdatedAt DESC;";

                    using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                patientList.Add(new PatientListItem
                                {
                                    Id = reader["Id"]?.ToString() ?? "",
                                    Gender = reader["Gender"]?.ToString() ?? "",
                                    Age = reader["Age"]?.ToString() ?? "",
                                    AtrialFibrillationType = reader["AtrialFibrillationType"]?.ToString() ?? "",
                                    Hypertension = reader["Hypertension"]?.ToString() ?? "",
                                    Diabetes = reader["Diabetes"]?.ToString() ?? "",
                                    CreatedAt = reader["CreatedAt"]?.ToString() ?? "",
                                    UpdatedAt = reader["UpdatedAt"]?.ToString() ?? ""
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"患者リスト取得中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return patientList;
        }

        /// <summary>
        /// 患者を削除
        /// </summary>
        /// <param name="patientId">削除する患者のID</param>
        /// <returns>削除の成否</returns>
        public bool DeletePatient(string patientId)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    string sql = "DELETE FROM patients WHERE Id = @Id;";
                    using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", patientId);
                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"患者データ削除中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }

    /// <summary>
    /// 患者リスト表示用のデータクラス
    /// </summary>
    public class PatientListItem
    {
        public string Id { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string AtrialFibrillationType { get; set; }
        public string Hypertension { get; set; }
        public string Diabetes { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}