import React, { Component } from 'react'
import Input from './Input'

export class Employee extends Component {
  
  state = {
    employees: [],
    loading: true,    
  }

  // Load Employee data upon load
  componentDidMount() {
    this.populateEmployeeData();
  }  

  // Button handlers for Input component and deduction button respectively
  handleUpdates = (fname, lname, depts) => {    
    this.addEmployee(fname, lname, depts);
  }

  handleDeduct = (fname, lname) => {
    this.GetCalculatedDeductions(fname, lname);
  }  
  
  render() {
    const { employees, loading } = this.state;
    return (      
      <div>
      {loading ? "Loading..." : 
        <>
          <table className="table table-striped">
            <thead>
              <tr>
                <th>First Name</th>
                <th>Last Name</th>
                <th>Dependents</th>
                <th></th>          
              </tr>
            </thead>
            <tbody>
            {employees.$values.map((employee, i) =>               
              <tr key={employee.id}>
                <td>{employee.firstName}</td>
                <td>{employee.lastName}</td>
                <td>
                  {employee.employeeDependents.$values.length > 0 ? 
                    <ul key={employee.id + i}>
                      {employee.employeeDependents.$values.map(dependent =>
                        <li key={dependent.id}>{dependent.firstName} {dependent.lastName}</li>
                      )}
                    </ul> 
                  : null}
                </td>
                <td>
                  <button onClick={() => this.handleDeduct(employee.firstName, employee.lastName)}>Calculate Deductions</button>
                </td>
              </tr>          
            )}
            </tbody>
          </table>
          <Input 
            handleUpdates={this.handleUpdates}
          />
        </>
      }
      </div>
    )
  }

  async populateEmployeeData() {
    const response = await fetch('employee');
    const data = await response.json();
    this.setState({ employees: data, loading: false });
  }

  async addEmployee(fname, lname, deps) {
    
    debugger;
    let empData = {
      firstName: fname,
      lastName: lname,
      dependents: deps
    }    

    const response = await fetch('employee', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(empData)
    });
    const data = await response.json();
    this.setState({ employees: data, loading: false });
  }

  async GetCalculatedDeductions(fname, lname) {

    let empData = {
      firstName: fname,
      lastName: lname      
    }   

    const response = await fetch('employee/CalculateDeduction', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(empData)
    });

    const data = await response.text();
    alert(data);
  }

  // Would have more handlers if of edit/delete employees and/or dependents if given more time
  async deleteEmployee() {

  }

  async editEmployee() {

  }

  async deleteEmployeeDependent() {

  }

  async editEmployeeDependent() {

  }
  
}

export default Employee