import React, { Component } from 'react'

export class Input extends Component {

  state = {
    inputShown: false,
    firstName: "",
    lastName: "",
    dependentFname: "",
    dependentLname: "",
    dependents: []
  }

  handleSubmit = (event) => {
    event.preventDefault();

    // clear out dependents, fname, lname
    this.setState({
      dependents: [],
      firstName: "",
      lastName: ""
    });

    this.props.handleUpdates(this.state.firstName, this.state.lastName, this.state.dependents);
  }

  showDependentInput = () => {    
    this.setState({inputShown: !this.state.inputShown});
  }

  addDependent = () => {
    this.setState({
      dependents: [...this.state.dependents, {firstName: this.state.dependentFname, lastName: this.state.dependentLname}],
      dependentFname: "",
      dependentLname: "",
      inputShown: false
    });
  }

  // I would further refactor these input components into another React component if given more time
  // Also, would have some proper input handling within said component to not have empty or invalid inputs
  render() {
    const { firstName, lastName, dependentFname, dependentLname, dependents, inputShown } = this.state;
    return (
      <>
        <form onSubmit={this.handleSubmit}>
          <div className="container">

            <div className="row">
              <div className="col">
                <label htmlFor="fname">First Name</label>
                <input 
                  id="fname"
                  type="text"
                  className="form-control"
                  placeholder="Enter First Name"
                  onChange = {e => this.setState({ firstName: e.target.value })}
                  value={firstName} 
                />
              </div>
              <div className="col">
                <label htmlFor="lname">Last Name</label>
                <input 
                  id="lname"
                  type="text"
                  className="form-control"
                  placeholder="Enter Last Name"
                  onChange = {e => this.setState({ lastName: e.target.value })}
                  value={lastName} 
                />
              </div>

              {/* Ran out of time for this implementation, however there would be similar associations to what type benefit is added, per employee and per dependent */}
              <div className="col">
                <label htmlFor="election">Election Type</label>
                <select name="election" id="election" className="form-select">
                  <option value="401k">401k</option>
                  <option value="HealthBenefits">Health Benefits</option>                  
                </select>
              </div>
            </div>


            {/* list of dependents */}
            {dependents.length > 0 ? 
            <div className="row">
              <div className="col">
              <ul>
                {dependents.map((dependent, i) => 
                  <li key={dependent.firstName + i}>{dependent.firstName} {dependent.lastName}</li>
                )}
              </ul>
              </div>
            </div>
            : null}

            <div className="row mt-3">
              <div className="col">
                <button 
                  type='button'
                  className="btn btn-primary"
                  onClick={this.showDependentInput}
                  hidden={inputShown}
                >
                    Add dependent +
                </button>
              </div>
            </div>
            
            {/* Dependent Input section */}
            { inputShown ? 
            <>
            <div className="row">
              <div className="col">
                <label htmlFor="dfname">Dependent First Name</label>
                <input 
                  id="dfname"
                  type="text"
                  className="form-control"
                  placeholder="Enter Dependent's First Name"
                  onChange = {e => this.setState({ dependentFname: e.target.value })}
                  value={dependentFname}                   
                />
              </div>
              <div className="col">
                <label htmlFor="dlname">Dependent Last Name</label>
                <input 
                  id="dlname"
                  type="text"
                  className="form-control"
                  placeholder="Enter Dependent's Last Name"
                  onChange = {e => this.setState({ dependentLname: e.target.value })}
                  value={dependentLname} 
                  minLength={1}
                />
              </div>

              <div className="row mt-3">
                <div className="col-2">
                  <button 
                    type="button"
                    className="btn btn-primary px-2"
                    onClick={this.addDependent}                    
                  >
                    Enter Dependent Info
                  </button>
                </div>
                <div className="col-2">
                  <button 
                    type="button"
                    className="btn btn-danger"
                    onClick={this.showDependentInput}                    
                  >
                    Cancel
                  </button>
                </div>                
              </div>
            
            </div> 
            </>
            : null}

            {!inputShown ? 
              <div className="row mt-3">
                <div className="col">
                  <button type="submit" className="btn btn-primary">Submit</button>
                </div>
              </div>
            : null }

          </div>

        </form>
      </>
    )
  }
}

export default Input