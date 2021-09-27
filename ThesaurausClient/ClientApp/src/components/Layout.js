import React, { Component } from 'react';
import { NavMenu } from './NavMenu';
import {  Container,  NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import '../NavMenu.css';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <NavMenu />
            <Container>
                <div className="row">

                <div className="col-sm-4">
                    <ul className="navbar-nav">
                       
                        <NavItem>
                            <NavLink tag={Link} className="text-dark" to="/getsynonyms">GET Synonym</NavLink>
                        </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/getallwords" >GET All words</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={Link} className="text-dark" to="/post">ADD</NavLink>
                        </NavItem>
                    </ul>
                </div>
                <div className="col-sm-6">

                    {this.props.children}
                    </div>

                </div>
        </Container>
      </div>
    );
  }
}
