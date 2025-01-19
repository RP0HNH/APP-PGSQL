# **C#+PGSQL**: �������� ��������

## **�������� �������**
��� ���������� �� ����� C# � �������������� Windows Forms, ������� ������������ � ���� ������ PostgreSQL � ��������� ���������� ������� ����������� � �������� ������ ����� �������� ���������. ���������� ��������� �������� ������ �� ���� ������ � ������� � ����������� ����������, � ����� ��������� �������� �� ��������� ������.

## **����������� ����������**
1. **���� ����������������**: C#.
2. **����������**:
   - Windows Forms.
   - PostgreSQL.
3. **����������**:
   - ���������� ������ ������������ � ���� ������ PostgreSQL � ��������� ������� � �������������� �������� ���������.
   - ������� ������ (��������, ID ������ � ������� ��������� ��������) �������� � �����.
   - ������ � ���� ������ ������ ����������� ��� ������� ������ ��� ������� Enter.
   - ���������� ������� ������ ���� ���������� � ������� �� �����.

## **��� ��������� ������**

### 1. ��������� ���� ������
1. ���������, ��� � ��� ���������� PostgreSQL.
2. �������� ���� ������.
3. �������� ������� � �������� ���������, ��������� SQL-�������, �������������� ����.
4. �������� ������ � �������.

```sql
-- �������� ������� DEPARTMENT
CREATE TABLE DEPARTMENT (
    ID SERIAL PRIMARY KEY,  
    NAME VARCHAR(100) NOT NULL  
);

-- �������� ������� EMPLOYEE
CREATE TABLE EMPLOYEE (
    ID SERIAL PRIMARY KEY,  
    DEPARTMENT_ID INT NOT NULL,  
    CHIEF_ID INT NULL,  
    NAME VARCHAR(100) NOT NULL,  
    SALARY NUMERIC(10, 2) NOT NULL,  
    CONSTRAINT FK_DEPARTMENT FOREIGN KEY (DEPARTMENT_ID) REFERENCES DEPARTMENT(ID) ON DELETE CASCADE,  
    CONSTRAINT FK_CHIEF FOREIGN KEY (CHIEF_ID) REFERENCES EMPLOYEE(ID) ON DELETE SET NULL  
);

-- ������� ������ � ������� DEPARTMENT
INSERT INTO DEPARTMENT (NAME) 
VALUES
    ('IT'),
    ('HR'),
    ('Sales');

-- ������� ������ � ������� EMPLOYEE
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

### 2. ����������� � ���� ������
1. �������� ������ � Visual Studio.
2. �������� ���� `Form1` � ���������, ��� ������ ����������� � ���� ������ ������� �����:

<connectionStrings>
  < string connectionString = "Host=________;Port=_______;Username=_______;Password=_______;Database=_______;";>
</connectionStrings>


### 3. ������ ����������
1. �������� � ��������� ������ � Visual Studio.
2. �� ����� �������� ������� ��� ����������� ������ �����������.
3. ������� ID ������ � ������� ��������� �������� � ��������� ����.
4. ������� ������ "���������" ��� ���������� �������.
5. ���������� ����������� ������� ����� ���������� � ������� �� �����.

## **���������� ����������**
1. ���������� ���������� ������ ����������� �� ���� ������ � �������.
2. ������������ ����� ������ ID ������ � ������� ��������� ��������.
3. ��� ������� ������ ��� ������� Enter ����������� ������ � ���� ������, ������� ��������� �������� ����������� � ��������� ������.
4. ���������� ������� (����� � ������ �������� �����������) ������������ � ������� �� �����.

## **���������� �������� ���������**
�������� ��������� `UPDATESALARYFORDEPARTMENT` ��������� �������� ����������� � �������� ������. ��������� ��������� ��� ���������: ID ������ � ������� ��������� ��������.

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

## **������������ ����������**
- **Npgsql**: ��� ����������� � ������ � ����� ������ PostgreSQL.

## **�������� � ��������� �����������**
- � ���������� ������������� ������� ��������� ��������� ������, ������ �������������� �������� �� ������� ������� ����� ���� ��������� � �������.

## **��������**
��� �������� � ���������, ����������, ����������� �� ������ [your_email@example.com].

---

���� ���� README ��������� ��� ������, ��� ����, ���� ��� ������� � ����������. �� ������ ��������� ��� �������������� �����������, ���� �����.