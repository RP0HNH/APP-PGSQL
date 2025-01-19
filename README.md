# **C#+PGSQL**: Тестовое залдание

## **Описание проекта**
Это приложение на языке C# с использованием Windows Forms, которое подключается к базе данных PostgreSQL и выполняет обновление зарплат сотрудников в заданном отделе через хранимую процедуру. Приложение позволяет выводить данные из базы данных в таблицу в графическом интерфейсе, а также обновлять зарплаты по введенным данным.

## **Технические требования**
1. **Язык программирования**: C#.
2. **Технологии**:
   - Windows Forms.
   - PostgreSQL.
3. **Требования**:
   - Приложение должно подключаться к базе данных PostgreSQL и выполнять запросы с использованием хранимой процедуры.
   - Входные данные (например, ID отдела и процент повышения зарплаты) задаются с формы.
   - Запрос к базе данных должен выполняться при нажатии кнопки или клавиши Enter.
   - Результаты запроса должны быть отображены в таблице на форме.

## **Как запустить проект**

### 1. Настройка базы данных
1. Убедитесь, что у вас установлен PostgreSQL.
2. Создайте базу данных.
3. Создайте таблицы и хранимую процедуру, используя SQL-скрипты, представленные ниже.
4. Вставьте данные в таблицы.

```sql
-- Создание таблицы DEPARTMENT
CREATE TABLE DEPARTMENT (
    ID SERIAL PRIMARY KEY,  
    NAME VARCHAR(100) NOT NULL  
);

-- Создание таблицы EMPLOYEE
CREATE TABLE EMPLOYEE (
    ID SERIAL PRIMARY KEY,  
    DEPARTMENT_ID INT NOT NULL,  
    CHIEF_ID INT NULL,  
    NAME VARCHAR(100) NOT NULL,  
    SALARY NUMERIC(10, 2) NOT NULL,  
    CONSTRAINT FK_DEPARTMENT FOREIGN KEY (DEPARTMENT_ID) REFERENCES DEPARTMENT(ID) ON DELETE CASCADE,  
    CONSTRAINT FK_CHIEF FOREIGN KEY (CHIEF_ID) REFERENCES EMPLOYEE(ID) ON DELETE SET NULL  
);

-- Вставка данных в таблицу DEPARTMENT
INSERT INTO DEPARTMENT (NAME) 
VALUES
    ('IT'),
    ('HR'),
    ('Sales');

-- Вставка данных в таблицу EMPLOYEE
INSERT INTO EMPLOYEE (DEPARTMENT_ID, CHIEF_ID, NAME, SALARY)
VALUES
    (1, NULL, 'Alice Johnson', 120000),
    (2, NULL, 'Bob Smith', 110000),
    (3, NULL, 'Carol Davis', 130000),
    (1, 1, 'David Brown', 70000),
    (1, 1, 'Emma Wilson', 75000),
    (1, 1, 'Frank Miller', 68000),
    (1, 1, 'Grace Taylor', 60000),
    (1, 1, 'Hank Anderson', 62000),
    (2, 2, 'Ivy Martin', 58000),
    (3, 3, 'Jack White', 80000),
    (3, 3, 'Kara Clark', 85000),
    (3, 3, 'Leo Hall', 82000),
    (1, 1, 'Mia Young', 70000),
    (2, 2, 'Noah Adams', 62000),
    (3, 3, 'Olivia Scott', 83000);
```

### 2. Подключение к базе данных
1. Откройте проект в Visual Studio.
2. Откройте файл `Form1` и убедитесь, что строка подключения к базе данных указана верно:

<connectionStrings>
  < string connectionString = "Host=________;Port=_______;Username=_______;Password=_______;Database=_______;";>
</connectionStrings>


### 3. Запуск приложения
1. Соберите и запустите проект в Visual Studio.
2. На форме появится таблица для отображения данных сотрудников.
3. Введите ID отдела и процент повышения зарплаты в текстовые поля.
4. Нажмите кнопку "Выполнить" для выполнения запроса.
5. Результаты обновленных зарплат будут отображены в таблице на форме.

## **Функционал приложения**
1. Приложение отображает данные сотрудников из базы данных в таблице.
2. Пользователь может ввести ID отдела и процент повышения зарплаты.
3. При нажатии кнопки или клавиши Enter выполняется запрос к базе данных, который обновляет зарплаты сотрудников в выбранном отделе.
4. Результаты запроса (новая и старая зарплата сотрудников) отображаются в таблице на форме.

## **Реализация хранимой процедуры**
Хранимая процедура `UPDATESALARYFORDEPARTMENT` обновляет зарплаты сотрудников в заданном отделе. Процедура принимает два параметра: ID отдела и процент повышения зарплаты.

```sql
CREATE OR REPLACE FUNCTION UPDATESALARYFORDEPARTMENT (
    DepartmentID INT,  
    Percent NUMERIC(5, 2)  
)
RETURNS TABLE (
    ID INT,
    NAME VARCHAR,
    DEPARTMENT_ID INT,
    NewSalary NUMERIC(10, 2),
    OldSalary NUMERIC(10, 2)
) 
LANGUAGE plpgsql
AS $$
DECLARE
    ChiefID INT;  
BEGIN
    SELECT e.ID INTO ChiefID
    FROM EMPLOYEE e
    WHERE e.DEPARTMENT_ID = DepartmentID AND e.CHIEF_ID IS NULL;

    UPDATE EMPLOYEE e
    SET SALARY = e.SALARY + (e.SALARY * Percent / 100)
    WHERE e.DEPARTMENT_ID = DepartmentID AND e.ID != ChiefID;

    UPDATE EMPLOYEE e
    SET SALARY = (
        SELECT MAX(e2.SALARY)
        FROM EMPLOYEE e2
        WHERE e2.DEPARTMENT_ID = DepartmentID AND e2.ID != ChiefID
    )
    WHERE e.ID = ChiefID AND e.SALARY < (
        SELECT MAX(e2.SALARY)
        FROM EMPLOYEE e2
        WHERE e2.DEPARTMENT_ID = DepartmentID AND e2.ID != ChiefID
    );

    RETURN QUERY
    SELECT 
        e.ID, 
        e.NAME, 
        e.DEPARTMENT_ID, 
        e.SALARY AS NewSalary,
        ROUND(e.SALARY / (1 + Percent / 100), 2) AS OldSalary
    FROM EMPLOYEE e
    WHERE e.DEPARTMENT_ID = DepartmentID;
END;
$$;
```

## **Используемые библиотеки**
- **Npgsql**: для подключения и работы с базой данных PostgreSQL.

## **Проблемы и известные ограничения**
- В приложении предусмотрена базовая валидация введенных данных, однако дополнительные проверки на стороне клиента могут быть добавлены в будущем.

## **Контакты**
Для вопросов и поддержки, пожалуйста, обращайтесь по адресу [your_email@example.com].

---

Этот файл README описывает ваш проект, его цели, шаги для запуска и функционал. Вы можете дополнить его дополнительной информацией, если нужно.