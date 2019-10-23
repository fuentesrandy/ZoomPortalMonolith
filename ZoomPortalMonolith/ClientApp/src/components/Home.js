import React from 'react';
import { connect } from 'react-redux';

const Home = props => (
  <div>
    <h1>Hello, world!</h1>
            <a href="/swagger" target="new">Swagger</a>
  </div>
);

export default connect()(Home);
