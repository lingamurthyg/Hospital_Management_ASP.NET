-- PostgreSQL Sample Data Insertions for Clinic Management System
-- Database: DBProject

-- Clean existing data (if needed)
/*
DELETE FROM treatment_history;
DELETE FROM bill;
DELETE FROM appointment;
DELETE FROM time_slot;
DELETE FROM doctor;
DELETE FROM patient;
DELETE FROM other_staff;
DELETE FROM department;
DELETE FROM admin;
*/

-- ADMIN INSERTION
INSERT INTO admin (email, password, name, created_date, is_active) 
VALUES ('admin@clinic.com', 'admin', 'System Administrator', CURRENT_TIMESTAMP, true);

-- DEPARTMENT INSERTIONS
INSERT INTO department (dept_no, dept_name, description, created_date, is_active) VALUES
(1, 'Cardiology', 'We have the best heart specialists in town. Each one of them is very competent and experienced.', CURRENT_TIMESTAMP, true),
(2, 'Orthopaedics', 'Orthopedic surgeons use surgical means to treat musculoskeletal trauma, infections, tumors. We believe in the best.', CURRENT_TIMESTAMP, true),
(3, 'Ears Nose Throat', 'They are gentle. And are trained to handle kids as well as adults.', CURRENT_TIMESTAMP, true),
(4, 'Physiotherapy', 'Physiotherapists work through physical therapies such as exercise, and manipulation of bones, joints and muscle tissues.', CURRENT_TIMESTAMP, true),
(5, 'Neurology', 'A medical speciality dealing with disorders of the nervous system. It deals with the diagnosis and treatment of all categories of disease.', CURRENT_TIMESTAMP, true);

-- Reset sequence for department
SELECT setval('department_dept_no_seq', (SELECT MAX(dept_no) FROM department));

-- DOCTOR INSERTIONS
INSERT INTO doctor (name, email, password, birth_date, dept_no, gender, address, experience, salary, charges_per_visit, phone, qualification, specialization, reputation_index, patients_treated, status, created_date) VALUES
('Farhan Shoukat', 'farhan@gmail.com', 'abc', '1996-12-04', 1, 'Male', 'Enjoy, Lahore', 10, 30000, 2500, '156133213', 'PHD IN EVERY FIELD KNOWN TO MAN', 'ENJOY', 4.0, 0, true, CURRENT_TIMESTAMP),
('Kashan Ahmed', 'kashan@gmail.com', 'abc', '1996-12-12', 1, 'Male', 'Enjoy, Lahore', 10, 25000, 3000, '156133213', 'PHD IN EVERY FIELD KNOWN TO MAN', 'ENJOY', 3.5, 0, true, CURRENT_TIMESTAMP),
('Hassaan Ali', 'hassaan@gmail.com', 'abc', '1996-12-12', 1, 'Male', 'Enjoy, Lahore', 10, 20000, 1500, '156133213', 'PHD IN EVERY FIELD KNOWN TO MAN', 'ENJOY', 5.0, 0, true, CURRENT_TIMESTAMP),
('Haris Muneer', 'haris@gmail.com', 'abc', '1990-04-05', 1, 'Male', 'Enjoy, Lahore', 10, 15000, 1000, '156133213', 'PHD IN EVERY FIELD KNOWN TO MAN', 'ENJOY', 4.5, 0, true, CURRENT_TIMESTAMP),
('Talha Muneer', 'talha@gmail.com', 'abc', '1990-04-05', 2, 'Male', 'Enjoy, Lahore', 10, 15000, 1000, '156133213', 'PHD IN EVERY FIELD KNOWN TO MAN', 'ENJOY', 4.5, 0, true, CURRENT_TIMESTAMP),
('Shariq Muneer', 'shariq@gmail.com', 'abc', '1990-04-05', 2, 'Male', 'Enjoy, Lahore', 10, 15000, 1000, '156133213', 'PHD IN EVERY FIELD KNOWN TO MAN', 'ENJOY', 4.5, 0, true, CURRENT_TIMESTAMP),
('Awais Muneer', 'awais@gmail.com', 'abc', '1990-04-05', 3, 'Male', 'Enjoy, Lahore', 10, 15000, 1000, '156133213', 'PHD IN EVERY FIELD KNOWN TO MAN', 'ENJOY', 4.5, 0, true, CURRENT_TIMESTAMP),
('Alvi Khan', 'alvi@gmail.com', 'abc', '1990-04-05', 3, 'Male', 'Enjoy, Lahore', 10, 15000, 1000, '156133213', 'PHD IN EVERY FIELD KNOWN TO MAN', 'ENJOY', 4.5, 0, true, CURRENT_TIMESTAMP),
('Saifi Ahmed', 'saifi@gmail.com', 'abc', '1990-04-05', 4, 'Male', 'Enjoy, Lahore', 10, 15000, 1000, '156133213', 'PHD IN EVERY FIELD KNOWN TO MAN', 'ENJOY', 4.5, 0, true, CURRENT_TIMESTAMP),
('Mansha Ali', 'mansha@gmail.com', 'abc', '1990-04-05', 5, 'Male', 'Enjoy, Lahore', 10, 15000, 1000, '156133213', 'PHD IN EVERY FIELD KNOWN TO MAN', 'ENJOY', 4.5, 0, true, CURRENT_TIMESTAMP);

-- PATIENT INSERTIONS
INSERT INTO patient (name, email, password, phone, address, birth_date, gender, created_date, is_active) VALUES
('ABC Patient', 'ABC@gmail.com', 'abc', '61536516', 'ENJOY, LAHORE', '1996-04-04', 'Male', CURRENT_TIMESTAMP, true),
('DEF Patient', 'DEF@gmail.com', 'abc', '61536516', 'ENJOY, LAHORE', '1996-04-04', 'Male', CURRENT_TIMESTAMP, true),
('XYZ Patient', 'XYZ@gmail.com', 'abc', '61536516', 'ENJOY, LAHORE', '1996-04-04', 'Male', CURRENT_TIMESTAMP, true);

-- OTHER STAFF INSERTIONS
INSERT INTO other_staff (name, phone, address, designation, gender, birth_date, qualification, salary, created_date, is_active) VALUES
('Javed', '03227561002', 'Iqbal Town, Lhr', 'Guard', 'Male', '1990-04-05', 'Matric', 5000, CURRENT_TIMESTAMP, true),
('Hamza', '03227561002', 'Iqbal Town, Lhr', 'Sweeper', 'Male', '1990-04-05', 'Matric', 5000, CURRENT_TIMESTAMP, true),
('Kashan', '03227561002', 'Iqbal Town, Lhr', 'Security', 'Male', '1990-04-05', 'Matric', 5000, CURRENT_TIMESTAMP, true),
('Alio', '03227561002', 'Iqbal Town, Lhr', 'Guard', 'Male', '1990-04-05', 'Matric', 5000, CURRENT_TIMESTAMP, true),
('Kaleem', '03227561002', 'Iqbal Town, Lhr', 'Guard', 'Male', '1990-04-05', 'Matric', 5000, CURRENT_TIMESTAMP, true),
('Ali', '03227561002', 'Iqbal Town, Lhr', 'Guard', 'Male', '1990-04-05', 'Matric', 5000, CURRENT_TIMESTAMP, true);

-- TIME SLOT INSERTIONS (Sample time slots for doctors)
-- Get doctor IDs and create time slots
DO $$
DECLARE
    v_doctor_id INTEGER;
BEGIN
    -- Create time slots for each doctor
    FOR v_doctor_id IN SELECT doctor_id FROM doctor LOOP
        INSERT INTO time_slot (doctor_id, timings, is_available, created_date) VALUES
        (v_doctor_id, '09:00 AM - 10:00 AM', true, CURRENT_TIMESTAMP),
        (v_doctor_id, '10:00 AM - 11:00 AM', true, CURRENT_TIMESTAMP),
        (v_doctor_id, '11:00 AM - 12:00 PM', true, CURRENT_TIMESTAMP),
        (v_doctor_id, '02:00 PM - 03:00 PM', true, CURRENT_TIMESTAMP),
        (v_doctor_id, '03:00 PM - 04:00 PM', true, CURRENT_TIMESTAMP),
        (v_doctor_id, '04:00 PM - 05:00 PM', true, CURRENT_TIMESTAMP);
    END LOOP;
END $$;

-- SAMPLE APPOINTMENT INSERTIONS (commented out - can be uncommented when needed)
/*
DO $$
DECLARE
    v_doctor_id INTEGER;
    v_patient_id INTEGER;
    v_timeslot_id INTEGER;
BEGIN
    -- Get IDs
    SELECT doctor_id INTO v_doctor_id FROM doctor WHERE email = 'farhan@gmail.com';
    SELECT patient_id INTO v_patient_id FROM patient WHERE email = 'ABC@gmail.com';
    SELECT time_slot_id INTO v_timeslot_id FROM time_slot WHERE doctor_id = v_doctor_id LIMIT 1;
    
    -- Insert appointment
    INSERT INTO appointment (doctor_id, patient_id, time_slot_id, appointment_date, status, created_date) 
    VALUES (v_doctor_id, v_patient_id, v_timeslot_id, '2024-05-04 10:00:00', 'Completed', CURRENT_TIMESTAMP);
    
    -- Get IDs for second appointment
    SELECT doctor_id INTO v_doctor_id FROM doctor WHERE email = 'farhan@gmail.com';
    SELECT patient_id INTO v_patient_id FROM patient WHERE email = 'DEF@gmail.com';
    SELECT time_slot_id INTO v_timeslot_id FROM time_slot WHERE doctor_id = v_doctor_id OFFSET 1 LIMIT 1;
    
    INSERT INTO appointment (doctor_id, patient_id, time_slot_id, appointment_date, status, created_date) 
    VALUES (v_doctor_id, v_patient_id, v_timeslot_id, '2024-05-04 12:00:00', 'Approved', CURRENT_TIMESTAMP);
END $$;
*/

-- Verify insertions
SELECT 'Departments' as table_name, COUNT(*) as count FROM department
UNION ALL
SELECT 'Doctors', COUNT(*) FROM doctor
UNION ALL
SELECT 'Patients', COUNT(*) FROM patient
UNION ALL
SELECT 'Other Staff', COUNT(*) FROM other_staff
UNION ALL
SELECT 'Time Slots', COUNT(*) FROM time_slot
UNION ALL
SELECT 'Admins', COUNT(*) FROM admin;
