-- PostgreSQL Schema for Clinic Management System
-- Database: DBProject

-- Drop existing tables if they exist (in correct order due to foreign keys)
DROP TABLE IF EXISTS appointment CASCADE;
DROP TABLE IF EXISTS bill CASCADE;
DROP TABLE IF EXISTS treatment_history CASCADE;
DROP TABLE IF EXISTS time_slot CASCADE;
DROP TABLE IF EXISTS other_staff CASCADE;
DROP TABLE IF EXISTS doctor CASCADE;
DROP TABLE IF EXISTS patient CASCADE;
DROP TABLE IF EXISTS department CASCADE;
DROP TABLE IF EXISTS admin CASCADE;

--------------------------------------------------------
-------------------Creating Tables----------------------

-- Status Codes Reference:
-- Patient/Doctor Status: true = Active, false = Inactive
-- Appointment Status: 'Pending', 'Approved', 'Completed', 'Cancelled'
-- Bill Status: true = Paid, false = Unpaid

--------------------------------------------------------

-- Admin Table
CREATE TABLE admin
(
    admin_id SERIAL PRIMARY KEY,
    email VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    name VARCHAR(100) NOT NULL,
    created_date TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    modified_date TIMESTAMP WITHOUT TIME ZONE,
    is_active BOOLEAN DEFAULT true
);

-- Department Table
CREATE TABLE department
(
    dept_no SERIAL PRIMARY KEY,
    dept_name VARCHAR(100) NOT NULL UNIQUE,
    description VARCHAR(500),
    created_date TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    modified_date TIMESTAMP WITHOUT TIME ZONE,
    is_active BOOLEAN DEFAULT true
);

-- Patient Table
CREATE TABLE patient
(
    patient_id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    phone VARCHAR(20),
    address VARCHAR(200),
    birth_date TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    gender VARCHAR(10) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    created_date TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    modified_date TIMESTAMP WITHOUT TIME ZONE,
    is_active BOOLEAN DEFAULT true
);

-- Doctor Table
CREATE TABLE doctor
(
    doctor_id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    birth_date TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    dept_no INTEGER NOT NULL,
    gender VARCHAR(10) NOT NULL,
    address VARCHAR(200),
    experience INTEGER DEFAULT 0,
    salary NUMERIC(18,2),
    charges_per_visit NUMERIC(18,2) NOT NULL,
    phone VARCHAR(20),
    specialization VARCHAR(100),
    qualification VARCHAR(200) NOT NULL,
    reputation_index REAL DEFAULT 0.0,
    patients_treated INTEGER DEFAULT 0,
    status BOOLEAN DEFAULT true,
    created_date TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    modified_date TIMESTAMP WITHOUT TIME ZONE,
    
    CONSTRAINT fk_doctor_department FOREIGN KEY (dept_no) 
        REFERENCES department(dept_no) ON DELETE RESTRICT
);

-- Other Staff Table
CREATE TABLE other_staff
(
    staff_id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    phone VARCHAR(20),
    address VARCHAR(200),
    designation VARCHAR(100) NOT NULL,
    gender VARCHAR(10) NOT NULL,
    birth_date TIMESTAMP WITHOUT TIME ZONE,
    qualification VARCHAR(200),
    salary NUMERIC(18,2),
    created_date TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    modified_date TIMESTAMP WITHOUT TIME ZONE,
    is_active BOOLEAN DEFAULT true
);

-- Time Slot Table
CREATE TABLE time_slot
(
    time_slot_id SERIAL PRIMARY KEY,
    doctor_id INTEGER NOT NULL,
    timings VARCHAR(50) NOT NULL,
    is_available BOOLEAN DEFAULT true,
    created_date TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    modified_date TIMESTAMP WITHOUT TIME ZONE,
    
    CONSTRAINT fk_timeslot_doctor FOREIGN KEY (doctor_id) 
        REFERENCES doctor(doctor_id) ON DELETE CASCADE
);

-- Appointment Table
CREATE TABLE appointment
(
    appointment_id SERIAL PRIMARY KEY,
    patient_id INTEGER NOT NULL,
    doctor_id INTEGER NOT NULL,
    time_slot_id INTEGER NOT NULL,
    appointment_date TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    status VARCHAR(50) DEFAULT 'Pending',
    disease VARCHAR(200),
    progress VARCHAR(500),
    prescription VARCHAR(1000),
    feedback_given BOOLEAN DEFAULT false,
    created_date TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    modified_date TIMESTAMP WITHOUT TIME ZONE,
    
    CONSTRAINT fk_appointment_patient FOREIGN KEY (patient_id) 
        REFERENCES patient(patient_id) ON DELETE RESTRICT,
    CONSTRAINT fk_appointment_doctor FOREIGN KEY (doctor_id) 
        REFERENCES doctor(doctor_id) ON DELETE RESTRICT,
    CONSTRAINT fk_appointment_timeslot FOREIGN KEY (time_slot_id) 
        REFERENCES time_slot(time_slot_id) ON DELETE RESTRICT
);

-- Bill Table
CREATE TABLE bill
(
    bill_id SERIAL PRIMARY KEY,
    appointment_id INTEGER NOT NULL UNIQUE,
    patient_id INTEGER NOT NULL,
    amount NUMERIC(18,2) NOT NULL,
    is_paid BOOLEAN DEFAULT false,
    bill_date TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    paid_date TIMESTAMP WITHOUT TIME ZONE,
    created_date TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    modified_date TIMESTAMP WITHOUT TIME ZONE,
    
    CONSTRAINT fk_bill_appointment FOREIGN KEY (appointment_id) 
        REFERENCES appointment(appointment_id) ON DELETE RESTRICT,
    CONSTRAINT fk_bill_patient FOREIGN KEY (patient_id) 
        REFERENCES patient(patient_id) ON DELETE RESTRICT
);

-- Treatment History Table
CREATE TABLE treatment_history
(
    treatment_id SERIAL PRIMARY KEY,
    patient_id INTEGER NOT NULL,
    doctor_id INTEGER NOT NULL,
    appointment_id INTEGER NOT NULL,
    disease VARCHAR(200) NOT NULL,
    treatment VARCHAR(500),
    prescription VARCHAR(1000),
    treatment_date TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    created_date TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    modified_date TIMESTAMP WITHOUT TIME ZONE,
    
    CONSTRAINT fk_treatment_patient FOREIGN KEY (patient_id) 
        REFERENCES patient(patient_id) ON DELETE RESTRICT
);

-- Create indexes for better query performance
CREATE INDEX idx_patient_email ON patient(email);
CREATE INDEX idx_doctor_email ON doctor(email);
CREATE INDEX idx_doctor_dept ON doctor(dept_no);
CREATE INDEX idx_appointment_patient ON appointment(patient_id);
CREATE INDEX idx_appointment_doctor ON appointment(doctor_id);
CREATE INDEX idx_appointment_date ON appointment(appointment_date);
CREATE INDEX idx_bill_patient ON bill(patient_id);
CREATE INDEX idx_treatment_patient ON treatment_history(patient_id);

-- Comments for documentation
COMMENT ON TABLE admin IS 'Stores admin user information';
COMMENT ON TABLE department IS 'Stores department information';
COMMENT ON TABLE patient IS 'Stores patient information';
COMMENT ON TABLE doctor IS 'Stores doctor information';
COMMENT ON TABLE other_staff IS 'Stores other staff member information';
COMMENT ON TABLE time_slot IS 'Stores available time slots for doctors';
COMMENT ON TABLE appointment IS 'Stores appointment information';
COMMENT ON TABLE bill IS 'Stores billing information';
COMMENT ON TABLE treatment_history IS 'Stores patient treatment history';
