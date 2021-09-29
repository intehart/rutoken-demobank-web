import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { FormattedDate, FormattedMessage } from 'react-intl';

import { formatMoney } from '../utils';

import withOperation from '../withOperation';

import { generateSignature as signAction } from '../actions/sign';
import saveAction from '../actions/fileActions';

import PersonalSuccessSignModal from './PersonalSuccessSignModal';
import Button from '../controls/Button';


const PersonalBlockModal = ({ modalState, sign, save }) => (
    <div className="personal-payment-info">
        <h2>
            №&nbsp;
            {modalState.id}
        </h2>

        <div className="personal-payment-info--description">
            <FormattedMessage id="payment.account" />
            &nbsp;
            {modalState.id}
            &nbsp;
            <FormattedMessage id="payment.date-from" />
            &nbsp;
            {modalState.account.respondent.name}
        </div>

        <div className="personal-payment-info--field mt-3">
            <div className="personal-payment-info--label">
                <FormattedMessage id="payment.summ" />
            </div>
            <div className="personal-payment-info--value">
                {formatMoney(modalState.amount)}
            </div>
        </div>

        <div className="personal-payment-info--field mt-1">
            <div className="personal-payment-info--label">
                <FormattedMessage id="payment.date" />
            </div>
            <div className="personal-payment-info--value">
                <FormattedDate
                    value={modalState.paymentDate}
                    day="numeric"
                    month="long"
                    year="numeric"
                />
            </div>
        </div>

        {
            modalState.cms && <div className="personal-signature" />
        }

        <div className="personal-payment-info--field mt-2">
            <div className="personal-payment-info--label">
                <FormattedMessage id="payment.recipient-bank" />
            </div>
        </div>

        <h3>{modalState.account.bank.name}</h3>

        <div className="personal-payment-info--field mt-1">
            <div className="personal-payment-info--label">
                <FormattedMessage id="payment.account" />
            </div>
            <div className="personal-payment-info--value">
                {modalState.account.bank.checkingAccount}
            </div>
        </div>

        <div className="personal-payment-info--field mt-1">
            <div className="personal-payment-info--label">
                <FormattedMessage id="payment.bik" />
            </div>
            <div className="personal-payment-info--value">
                {modalState.account.bank.bik}
            </div>
        </div>

        <div className="personal-payment-info--field mt-2">
            <div className="personal-payment-info--label">
                <FormattedMessage id="payment.recipient" />
            </div>
        </div>

        <h3>{modalState.account.respondent.name}</h3>

        <div className="personal-payment-info--field mt-1">
            <div className="personal-payment-info--label">
                <FormattedMessage id="payment.account" />
            </div>
            <div className="personal-payment-info--value">
                {modalState.account.accountNumber}
            </div>
        </div>

        <div className="personal-payment-info--field mt-1">
            <div className="personal-payment-info--label">
                <FormattedMessage id="payment.inn" />
            </div>
            <div className="personal-payment-info--value">
                {modalState.account.respondent.inn}
            </div>
        </div>

        {
            !modalState.cms && (

                <div className="mt-2">
                    <Button type="button" className="btn" onClick={() => sign(modalState)}>
                        <FormattedMessage id="personal.sign-and-dispatch" />
                        <small>
                            <FormattedMessage id="personal.payment-with-summ" />
                            &nbsp;
                            {formatMoney(modalState.amount)}
                        </small>
                    </Button>
                </div>
            )
        }
        {
            modalState.cms && (
                <div className="mt-2">
                    <button type="button" className="btn" onClick={() => save(modalState)}>
                        <FormattedMessage id="personal.save-cms" />
                    </button>
                </div>
            )
        }

    </div>
);

const mapActionsToProps = (dispatch) =>
    ({
        sign: (payment) => dispatch(signAction(payment)),
        save: (payment) => dispatch(saveAction(payment.cms, 'signature.cms')),
    });

PersonalBlockModal.propTypes = {
    modalState: PropTypes.shape().isRequired,
    sign: PropTypes.func.isRequired,
    save: PropTypes.func.isRequired,
};

export default withOperation('sign', connect(null, mapActionsToProps)(PersonalBlockModal), PersonalSuccessSignModal);
