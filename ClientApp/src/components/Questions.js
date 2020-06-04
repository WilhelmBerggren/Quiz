import React, { Component } from 'react';

export class Questions extends Component {
  static displayName = Questions.name;

  constructor(props) {
    super(props);
    this.state = { questions: [], loading: true };
  }

  componentDidMount() {
    this.populateQuestionsData();
  }

  static renderQuestionsTable(questions) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Id</th>
            <th>Title</th>
            <th>Answer</th>
            <th>Option1</th>
            <th>Option2</th>
          </tr>
        </thead>
        <tbody>
          {questions.map(question =>
            <tr key={question.id}>
              <td>{question.id}</td>
              <td>{question.title}</td>
              <td>{question.answer}</td>
              <td>{question.option1}</td>
              <td>{question.option2}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Questions.renderQuestionsTable(this.state.questions);

    return (
      <div>
        <h1 id="tabelLabel" >Questions</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateQuestionsData() {
    const response = await fetch('api/QuizItems');
    const data = await response.json();
    this.setState({ questions: data, loading: false });
  }
}
