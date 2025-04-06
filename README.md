
# 💸 Control de Gastos Personales

Aplicación web desarrollada con ASP.NET Core MVC para llevar un registro de gastos personales, clasificados por categorías. 

---

## 🎯 Objetivos del Proyecto

- Aplicar principios como separación de responsabilidades e inyección de dependencias.
- Implementar funcionalidades como autenticación, validaciones, filtros entre otros.
- Crear una base para escalar el proyecto en el futuro

---

## ⚙️ Tecnologías y Herramientas

- **ASP.NET Core MVC**
- **Entity Framework Core**
- **SQL Server LocalDB**
- **C#**
- **Bootstrap 5** (para estilos rápidos)
- **LINQ** para consultas
- **Razor Views** para el frontend

---

## 🧠 Funcionalidades actuales

- Registro de gastos con:
  - Monto
  - Fecha
  - Categoría
- Gestión de categorías
- Listado y filtrado de gastos por fecha y categoría
- Validaciones básicas en formularios
- Uso de servicios para la lógica de negocio

---

## 🛣️ Funcionalidades planeadas

- [ ] Sistema de autenticación por usuario
- [ ] Filtro de gastos por usuario
- [ ] Exportación a Excel o PDF
- [ ] Carga de configuraciones desde `appsettings.json`
- [ ] Dashboard con resumen mensual (total por categoría)
- [ ] Paginación en las vistas
- [ ] Dark mode 😎

---

## 📁 Estructura del proyecto

```
ControlGastos/
├── Controllers/
├── Models/
├── Services/
├── Data/
├── Views/
├── wwwroot/
└── appsettings.json
```

---

## 🚀 Cómo correr el proyecto

1. Cloná el repo:
   ```bash
   git clone https://github.com/tuusuario/ControlGastos.git
   cd ControlGastos
   ```

2. Configurá la cadena de conexión en `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ControlGastosDb;Trusted_Connection=True;"
   }
   ```

3. Ejecutá las migraciones:
   ```bash
   dotnet ef database update
   ```

4. Levantá la app:
   ```bash
   dotnet run
   ```

---

## 🤝 Contribuciones

Este proyecto es para fines de aprendizaje, pero si tenés ideas o mejoras, ¡bienvenidas sean!

---

## 📬 Contacto

Si querés saber más sobre este proyecto, podés escribirme por GitHub o dejar un issue.

---
